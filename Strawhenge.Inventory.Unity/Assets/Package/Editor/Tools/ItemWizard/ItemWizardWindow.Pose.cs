using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.ItemData;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Strawhenge.Inventory.Unity.Editor.Tools.ItemWizard
{
    public partial class ItemWizardWindow
    {
        [SerializeField] ItemScriptableObject _poseItem;
        [SerializeField] InventoryScript _poseCharacter;
        [SerializeField] int _slotIndex;
        [SerializeField] AnimationClip _poseClip;
        [SerializeField, Range(0, 1)] float _poseClipTime;

        readonly OffsetPreview _preview = new();
        IReadOnlyList<PoseSlot> _slots = new List<PoseSlot>();
        PoseSlot _activeSlot;
        Vector3 _savedPosition;
        Vector3 _savedRotation;
        Vector3 _lastPosition;
        Vector3 _lastRotation;

        bool IsPosing => _preview.IsActive && _activeSlot != null;

        void LoadPoseItem(ItemScriptableObject item)
        {
            if (item == _poseItem)
                return;

            // Can be called outside OnGUI (from menus), so this uses a plain dialog rather than IfConfirmed.
            if (IsPosing && !ConfirmDiscard())
                return;

            StopPosing();
            _poseItem = item;
        }

        /// <summary>
        /// For use outside OnGUI. Returns true if there's nothing to lose or the user agrees to discard it.
        /// </summary>
        bool ConfirmDiscard() =>
            !HasUnappliedChanges() ||
            EditorUtility.DisplayDialog(
                "Unapplied offset",
                $"Discard the unapplied {_activeSlot.DisplayName} offset for '{_poseItem.name}'?",
                "Discard", "Keep Posing");

        void EnablePosing()
        {
            EditorApplication.update += OnEditorUpdate;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            EditorSceneManager.sceneClosing += OnSceneClosing;
            EditorApplication.hierarchyChanged += OnHierarchyChanged;
            RefreshSlots();
        }

        void DisablePosing()
        {
            EditorApplication.update -= OnEditorUpdate;
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorSceneManager.sceneClosing -= OnSceneClosing;
            EditorApplication.hierarchyChanged -= OnHierarchyChanged;
            StopPosing();
        }

        void DrawPose()
        {
            EditorGUILayout.LabelField("Pose Offsets", EditorStyles.boldLabel);

            EditorGUI.BeginChangeCheck();
            var item = (ItemScriptableObject)EditorGUILayout.ObjectField(
                "Item", _poseItem, typeof(ItemScriptableObject), allowSceneObjects: false);
            if (EditorGUI.EndChangeCheck())
            {
                IfConfirmed(() =>
                {
                    StopPosing();
                    _poseItem = item;
                });
            }

            DrawCharacterField();
            DrawSlotPopup();
            DrawPoseClip();

            if (IsPosing)
                DrawActivePose();
            else
                DrawStartPosing();
        }

        void DrawCharacterField()
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();
            var character = (InventoryScript)EditorGUILayout.ObjectField(
                new GUIContent("Character", "A character in the open scene with an InventoryScript."),
                _poseCharacter, typeof(InventoryScript), allowSceneObjects: true);
            if (EditorGUI.EndChangeCheck())
            {
                IfConfirmed(() =>
                {
                    StopPosing();
                    _poseCharacter = character;
                    RefreshSlots();
                });
            }

            if (_poseCharacter == null && GUILayout.Button("Find", GUILayout.Width(44)))
            {
                _poseCharacter = FindObjectOfType<InventoryScript>();
                RefreshSlots();

                if (_poseCharacter == null)
                    ShowNotification(new GUIContent("No InventoryScript in the open scene."));

                // The slot controls below appear or disappear, so redraw from the new state.
                GUIUtility.ExitGUI();
            }

            EditorGUILayout.EndHorizontal();
        }

        void DrawSlotPopup()
        {
            if (_poseCharacter == null)
                return;

            if (_slots.Count == 0)
            {
                EditorGUILayout.HelpBox(
                    "The character has no hands or holsters. Run Strawhenge > Inventory > Set Up Character... on it first.",
                    MessageType.Info);
                return;
            }

            var names = _slots
                .Select(slot => _poseItem != null && !slot.IsHand && !ItemOffsets.HasData(_poseItem, slot)
                    ? slot.DisplayName + " (new for this item)"
                    : slot.DisplayName)
                .ToArray();

            EditorGUI.BeginChangeCheck();
            var index = EditorGUILayout.Popup("Slot", Mathf.Clamp(_slotIndex, 0, _slots.Count - 1), names);
            if (EditorGUI.EndChangeCheck())
            {
                IfConfirmed(() =>
                {
                    _slotIndex = index;
                    if (IsPosing)
                        StartPosing();
                });
            }
        }

        void DrawPoseClip()
        {
            EditorGUI.BeginChangeCheck();

            _poseClip = (AnimationClip)EditorGUILayout.ObjectField(
                new GUIContent("Preview Pose", "Optional. An animation clip sampled onto the character while posing, " +
                                               "e.g. the hold animation, so the item is posed against the real pose."),
                _poseClip, typeof(AnimationClip), allowSceneObjects: false);

            using (new EditorGUI.DisabledScope(_poseClip == null))
                _poseClipTime = EditorGUILayout.Slider("Pose Time", _poseClipTime, 0, 1);

            if (EditorGUI.EndChangeCheck() && IsPosing)
                ApplyPoseClip();
        }

        void DrawStartPosing()
        {
            var problem = GetStartProblem();
            if (problem != null)
                EditorGUILayout.HelpBox(problem, MessageType.Info);

            using (new EditorGUI.DisabledScope(problem != null))
            {
                if (GUILayout.Button("Start Posing", GUILayout.Height(24)))
                    StartPosing();
            }
        }

        void DrawActivePose()
        {
            var transform = _preview.Transform;

            EditorGUILayout.HelpBox(
                $"Move and rotate '{transform.name}' in the Scene view, or type values below. " +
                "Apply saves them to the item.",
                MessageType.None);

            EditorGUI.BeginChangeCheck();
            var position = EditorGUILayout.Vector3Field("Position", transform.localPosition);
            var rotation = EditorGUILayout.Vector3Field("Rotation", transform.localEulerAngles);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(transform, "Pose Item Offset");
                _preview.Set(position, rotation);
            }

            if (_preview.IsScaleChanged)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.HelpBox("Scale isn't saved in offsets. Change the item's prefab to resize it.",
                    MessageType.Warning);
                if (GUILayout.Button("Reset Scale", GUILayout.Width(90), GUILayout.Height(38)))
                    _preview.ResetScale();
                EditorGUILayout.EndHorizontal();
            }

            var hasChanges = HasUnappliedChanges();

            EditorGUILayout.BeginHorizontal();
            using (new EditorGUI.DisabledScope(!hasChanges))
            {
                if (GUILayout.Button("Apply"))
                    ApplyOffset();

                if (GUILayout.Button("Revert"))
                    _preview.Set(_savedPosition, _savedRotation);
            }

            if (GUILayout.Button("Stop Posing"))
                IfConfirmed(StopPosing);
            EditorGUILayout.EndHorizontal();

            if (_activeSlot.IsHand)
                DrawCopyHands();
        }

        void DrawCopyHands()
        {
            var other = _activeSlot.Kind == PoseSlotKind.LeftHand ? PoseSlotKind.RightHand : PoseSlotKind.LeftHand;
            var thisName = _activeSlot.Kind == PoseSlotKind.LeftHand ? "Left" : "Right";
            var otherName = other == PoseSlotKind.LeftHand ? "Left" : "Right";

            EditorGUILayout.BeginHorizontal();

            using (new EditorGUI.DisabledScope(HasUnappliedChanges()))
            {
                if (GUILayout.Button(new GUIContent($"Copy {thisName} → {otherName}",
                        $"Copies the saved {thisName.ToLower()} hand offset to the {otherName.ToLower()} hand.")))
                {
                    ItemOffsets.CopyHand(_poseItem, _activeSlot.Kind, other);
                    AssetDatabase.SaveAssets();
                    ShowNotification(new GUIContent($"Copied to {otherName.ToLower()} hand."));
                }
            }

            if (GUILayout.Button(new GUIContent($"Copy {otherName} → {thisName}",
                    $"Replaces this hand's offset with the saved {otherName.ToLower()} hand offset.")))
            {
                ItemOffsets.CopyHand(_poseItem, other, _activeSlot.Kind);
                AssetDatabase.SaveAssets();
                LoadSavedOffset();
                _preview.Set(_savedPosition, _savedRotation);
            }

            EditorGUILayout.EndHorizontal();
        }

        string GetStartProblem()
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode)
                return "Posing is only available outside Play mode.";

            if (_poseItem == null)
                return "Choose an item, or create one above.";

            if (GetPrefab(_poseItem) == null)
                return "The item has no Prefab assigned.";

            if (_poseCharacter == null)
                return "Choose a character in the scene, or click Find.";

            if (_slots.Count == 0)
                return "The character has no hands or holsters.";

            return null;
        }

        void StartPosing()
        {
            if (GetStartProblem() != null)
                return;

            _activeSlot = _slots[Mathf.Clamp(_slotIndex, 0, _slots.Count - 1)];
            LoadSavedOffset();

            _preview.Start(GetPrefab(_poseItem), _activeSlot.Point, _savedPosition, _savedRotation);
            ApplyPoseClip();
            RememberPreviewValues();
        }

        void StopPosing()
        {
            _preview.Stop();
            _activeSlot = null;
        }

        void ApplyPoseClip()
        {
            var animator = _poseCharacter != null ? _poseCharacter.GetComponentInChildren<Animator>() : null;
            _preview.SetPose(animator, _poseClip, _poseClipTime);
        }

        void ApplyOffset()
        {
            var transform = _preview.Transform;
            ItemOffsets.Write(_poseItem, _activeSlot, transform.localPosition, transform.localEulerAngles);
            AssetDatabase.SaveAssets();

            LoadSavedOffset();
            ShowNotification(new GUIContent($"Saved {_activeSlot.DisplayName} offset."));
        }

        void LoadSavedOffset()
        {
            ItemOffsets.TryRead(_poseItem, _activeSlot, out _savedPosition, out _savedRotation);
        }

        bool HasUnappliedChanges()
        {
            if (!IsPosing)
                return false;

            var transform = _preview.Transform;
            return (transform.localPosition - _savedPosition).sqrMagnitude > 1e-10f ||
                   Quaternion.Angle(transform.localRotation, Quaternion.Euler(_savedRotation)) > 0.01f;
        }

        /// <summary>
        /// For use inside OnGUI. Runs the action, first asking whether to apply or discard unapplied changes,
        /// then ends the current GUI pass.
        /// </summary>
        /// <remarks>
        /// The action can change state the rest of the pass relies on (e.g. stopping posing clears the
        /// active slot), and a modal dialog breaks the layout pass, so the pass always ends here and the
        /// window redraws from the new state.
        /// </remarks>
        void IfConfirmed(System.Action action)
        {
            if (HasUnappliedChanges())
            {
                var choice = EditorUtility.DisplayDialogComplex(
                    "Unapplied offset",
                    $"The {_activeSlot.DisplayName} offset for '{_poseItem.name}' has changes that haven't been applied.",
                    "Apply", "Cancel", "Discard");

                if (choice == 0)
                    ApplyOffset();

                if (choice != 1)
                    action();
            }
            else
            {
                action();
            }

            if (Event.current != null)
                GUIUtility.ExitGUI();
        }

        void RefreshSlots()
        {
            _slots = PoseSlot.FindSlots(_poseCharacter);
        }

        void RememberPreviewValues()
        {
            if (!IsPosing)
                return;

            _lastPosition = _preview.Transform.localPosition;
            _lastRotation = _preview.Transform.localEulerAngles;
        }

        void OnEditorUpdate()
        {
            if (_activeSlot == null)
                return;

            // The preview or the character may have been deleted or unloaded.
            if (!_preview.IsActive || _activeSlot.Point == null)
            {
                StopPosing();
                Repaint();
                return;
            }

            var transform = _preview.Transform;
            if (transform.localPosition != _lastPosition || transform.localEulerAngles != _lastRotation)
            {
                RememberPreviewValues();
                Repaint();
            }
        }

        void OnPlayModeStateChanged(PlayModeStateChange change)
        {
            if (change == PlayModeStateChange.ExitingEditMode)
                StopPosing();
        }

        void OnSceneClosing(Scene scene, bool removingScene) => StopPosing();

        void OnHierarchyChanged()
        {
            // Picks up hands or holsters added to the character. Slots are fixed while posing.
            if (IsPosing)
                return;

            RefreshSlots();
            Repaint();
        }

        static ItemScript GetPrefab(ItemScriptableObject item)
        {
            var property = new SerializedObject(item).FindProperty(ItemScriptableObject.PrefabFieldName);
            return property?.objectReferenceValue as ItemScript;
        }
    }
}
