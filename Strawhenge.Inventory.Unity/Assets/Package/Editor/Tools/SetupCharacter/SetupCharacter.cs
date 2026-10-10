using Strawhenge.Common.Unity.Editor.Helpers;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Items;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Editor.Tools.SetupCharacter
{
    static class SetupCharacter
    {
        public const string DefaultLayerName = "Inventory";

        public static SetupCharacterReport Run(
            GameObject character,
            bool createInventoryLayer,
            string layerName,
            AvatarMask avatarMask,
            int maxStoredItemsWeight)
        {
            var report = new SetupCharacterReport();

            var animator = character.GetComponentInChildren<Animator>(includeInactive: true);
            if (animator == null)
            {
                report.Error($"No Animator found on '{character.name}' or its children. " +
                             "Add an Animator (with an Animator Controller) and run setup again.");
                return report;
            }

            Undo.SetCurrentGroupName("Set Up Inventory Character");
            var undoGroup = Undo.GetCurrentGroup();

            var inventory = character.GetComponent<InventoryScript>();
            if (inventory == null)
            {
                inventory = Undo.AddComponent<InventoryScript>(character);
                report.Done($"Added {nameof(InventoryScript)} to '{character.name}'.");
            }
            else
            {
                report.Skipped($"'{character.name}' already has an {nameof(InventoryScript)}.");
            }

            var leftHand = GetOrCreateHand<LeftHandScript>(
                character, animator, HumanBodyBones.LeftHand, "Left Hand Item Point", report);

            var rightHand = GetOrCreateHand<RightHandScript>(
                character, animator, HumanBodyBones.RightHand, "Right Hand Item Point", report);

            AssignInventoryFields(inventory, leftHand, rightHand, animator, maxStoredItemsWeight);
            report.Done($"Linked hands and Animator on {nameof(InventoryScript)} (existing references kept).");

            SetUpAnimatorController(animator, createInventoryLayer, layerName, avatarMask, report);

            Undo.CollapseUndoOperations(undoGroup);

            report.NextStep("Add HolsterScript components (e.g. on hip or back bones) and add them to the Holsters list.");
            report.NextStep("Add ApparelSlotScript components if you use apparel, and add them to the Apparel Slots list.");
            report.NextStep("Assign a Loot Drop Prefab if apparel pieces can be dropped.");
            report.NextStep("Create Item assets via Assets > Create > Strawhenge > Inventory > Item.");

            return report;
        }

        static T GetOrCreateHand<T>(
            GameObject character,
            Animator animator,
            HumanBodyBones bone,
            string objectName,
            SetupCharacterReport report)
            where T : HandScript
        {
            var existing = character.GetComponentInChildren<T>(includeInactive: true);
            if (existing != null)
            {
                report.Skipped($"Found existing {typeof(T).Name} on '{existing.name}'.");
                return existing;
            }

            Transform parent = null;
            if (animator.isHuman)
                parent = animator.GetBoneTransform(bone);

            if (parent == null)
            {
                parent = character.transform;
                report.Warning($"Could not find the humanoid {bone} bone, so '{objectName}' was created " +
                               $"under '{character.name}'. Move it under the character's hand bone.");
            }

            var handObject = new GameObject(objectName);
            Undo.RegisterCreatedObjectUndo(handObject, $"Create {objectName}");
            Undo.SetTransformParent(handObject.transform, parent, $"Parent {objectName}");
            handObject.transform.localPosition = Vector3.zero;
            handObject.transform.localRotation = Quaternion.identity;
            handObject.transform.localScale = Vector3.one;

            var hand = Undo.AddComponent<T>(handObject);
            report.Done($"Created '{objectName}' under '{parent.name}' with {typeof(T).Name}.");
            return hand;
        }

        static void AssignInventoryFields(
            InventoryScript inventory,
            LeftHandScript leftHand,
            RightHandScript rightHand,
            Animator animator,
            int maxStoredItemsWeight)
        {
            var serializedObject = new SerializedObject(inventory);

            SetIfEmpty(serializedObject.FindProperty(InventoryScript.LeftHandFieldName), leftHand);
            SetIfEmpty(serializedObject.FindProperty(InventoryScript.RightHandFieldName), rightHand);
            SetIfEmpty(serializedObject.FindProperty(InventoryScript.AnimatorFieldName), animator);

            var weightProperty = serializedObject.FindProperty(InventoryScript.MaxStoredItemsWeightFieldName);
            if (weightProperty != null && weightProperty.intValue == 0)
                weightProperty.intValue = Mathf.Max(0, maxStoredItemsWeight);

            serializedObject.ApplyModifiedProperties();

            static void SetIfEmpty(SerializedProperty property, Object value)
            {
                if (property != null && property.objectReferenceValue == null)
                    property.objectReferenceValue = value;
            }
        }

        static void SetUpAnimatorController(
            Animator animator,
            bool createInventoryLayer,
            string layerName,
            AvatarMask avatarMask,
            SetupCharacterReport report)
        {
            if (animator.runtimeAnimatorController == null)
            {
                report.Warning($"Animator on '{animator.name}' has no Animator Controller. Assign one, then run " +
                               "Strawhenge > Inventory > Inventory Animator Layer.");
                return;
            }

            if (!(animator.runtimeAnimatorController is AnimatorController controller))
            {
                report.Warning($"'{animator.runtimeAnimatorController.name}' is not an editable Animator Controller " +
                               "(it may be an Override Controller). Add the inventory layer to the base controller " +
                               "with Strawhenge > Inventory > Inventory Animator Layer.");
                return;
            }

            Undo.RecordObject(controller, "Set Up Inventory Animator");

            controller.EnsureParametersExist(
                (AnimatorParameters.HoldItemLeftId, AnimatorControllerParameterType.Int),
                (AnimatorParameters.HoldItemRightId, AnimatorControllerParameterType.Int));

            if (!createInventoryLayer)
            {
                EditorUtility.SetDirty(controller);
                AssetDatabase.SaveAssets();
                report.Skipped("Inventory animator layer not requested.");
                return;
            }

            if (controller.GetLayersContaining<DrawItemStateMachine>().Count > 0)
            {
                EditorUtility.SetDirty(controller);
                AssetDatabase.SaveAssets();
                report.Skipped($"'{controller.name}' already has an inventory layer.");
                return;
            }

            if (string.IsNullOrWhiteSpace(layerName))
                layerName = DefaultLayerName;

            CreateInventoryLayer.CreateInventoryLayer.Create(controller, layerName, avatarMask);
            report.Done($"Added '{layerName}' layer and inventory parameters to '{controller.name}'.");
        }
    }

    class SetupCharacterReport
    {
        readonly List<string> _done = new();
        readonly List<string> _skipped = new();
        readonly List<string> _warnings = new();
        readonly List<string> _errors = new();
        readonly List<string> _nextSteps = new();

        public bool HasErrors => _errors.Count > 0;

        public void Done(string message) => _done.Add(message);

        public void Skipped(string message) => _skipped.Add(message);

        public void Warning(string message) => _warnings.Add(message);

        public void Error(string message) => _errors.Add(message);

        public void NextStep(string message) => _nextSteps.Add(message);

        public override string ToString()
        {
            var builder = new System.Text.StringBuilder();
            Append("Errors", _errors);
            Append("Done", _done);
            Append("Already set up", _skipped);
            Append("Needs attention", _warnings);
            Append("Next steps", _nextSteps);
            return builder.ToString().TrimEnd();

            void Append(string heading, List<string> lines)
            {
                if (lines.Count == 0)
                    return;

                builder.AppendLine(heading + ":");
                foreach (var line in lines)
                    builder.AppendLine("  - " + line);
                builder.AppendLine();
            }
        }
    }
}
