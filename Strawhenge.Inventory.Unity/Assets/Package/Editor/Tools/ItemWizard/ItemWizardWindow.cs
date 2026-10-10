using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Unity.Items.ItemData;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Editor.Tools.ItemWizard
{
    public partial class ItemWizardWindow : EditorWindow
    {
        const string Title = "Item Wizard";

        enum Tab
        {
            Create,
            Pose
        }

        static readonly string[] TabNames = { "Create", "Pose" };

        /// <summary>
        /// Opens the wizard. With an Item asset selected it opens on the Pose tab with that item,
        /// otherwise on the last tab used.
        /// </summary>
        [MenuItem("Strawhenge/Inventory/Item Wizard...", priority = 1)]
        public static void Open()
        {
            if (Selection.activeObject is ItemScriptableObject item)
                OpenPose(item);
            else
                ShowWindow();
        }

        [MenuItem("Assets/Create/Strawhenge/Inventory/Item (Wizard)...", priority = 0)]
        static void OpenFromAssetsMenu() => ShowWindow().SwitchTab(Tab.Create);

        /// <summary>
        /// Opens the wizard on the Pose tab with the given item loaded.
        /// </summary>
        public static void OpenPose(ItemScriptableObject item)
        {
            var window = ShowWindow();
            window.SwitchTab(Tab.Pose);
            window.LoadPoseItem(item);
        }

        static ItemWizardWindow ShowWindow()
        {
            var window = GetWindow<ItemWizardWindow>(utility: false, title: Title, focus: true);
            window.minSize = new Vector2(360, 360);
            window._args.Folder = GetSelectedFolder();
            return window;
        }

        [SerializeField] CreateItemArgs _args = new();
        [SerializeField] Tab _tab;
        Vector2 _scroll;

        void OnEnable() => EnablePosing();

        void OnDisable() => DisablePosing();

        void OnGUI()
        {
            EditorGUILayout.Space(4);
            var tab = (Tab)GUILayout.Toolbar((int)_tab, TabNames, GUILayout.Height(24));
            if (tab != _tab)
            {
                SwitchTab(tab);

                // The rest of this pass was laid out for the old tab.
                GUIUtility.ExitGUI();
            }

            EditorGUILayout.Space(4);
            _scroll = EditorGUILayout.BeginScrollView(_scroll);

            if (_tab == Tab.Create)
            {
                DrawBasics();
                EditorGUILayout.Space();
                DrawCreate();
            }
            else
            {
                DrawPose();
            }

            EditorGUILayout.EndScrollView();
        }

        void SwitchTab(Tab tab)
        {
            if (tab == _tab)
                return;

            // Leaving the Pose tab ends posing, so the preview doesn't linger out of sight.
            if (_tab == Tab.Pose && IsPosing)
            {
                // Event.current is only set inside OnGUI, where IfConfirmed can end the GUI pass safely.
                if (Event.current != null)
                {
                    IfConfirmed(() =>
                    {
                        StopPosing();
                        _tab = tab;
                    });
                }
                else if (ConfirmDiscard())
                {
                    StopPosing();
                    _tab = tab;
                }

                return;
            }

            _tab = tab;
        }

        void DrawBasics()
        {
            EditorGUILayout.LabelField("Basics", EditorStyles.boldLabel);

            EditorGUI.BeginChangeCheck();
            var model = (GameObject)EditorGUILayout.ObjectField(
                new GUIContent("Model", "A model file or prefab. It's placed inside the item's prefabs."),
                _args.Model,
                typeof(GameObject),
                allowSceneObjects: false);

            if (EditorGUI.EndChangeCheck())
            {
                if (model != null && (string.IsNullOrWhiteSpace(_args.Name) || _args.Model != null && _args.Name == _args.Model.name))
                    _args.Name = model.name;

                _args.Model = model;
            }

            _args.Name = EditorGUILayout.TextField(
                new GUIContent("Name", "The item's name. Used for the asset names and to identify the item."),
                _args.Name);

            _args.Size = (ItemSize)EditorGUILayout.EnumPopup("Size", _args.Size);
            _args.IsStorable = EditorGUILayout.Toggle("Storable", _args.IsStorable);

            using (new EditorGUI.DisabledScope(!_args.IsStorable))
                _args.Weight = Mathf.Max(0, EditorGUILayout.IntField("Weight", _args.Weight));

            _args.CreatePickup = EditorGUILayout.Toggle(
                new GUIContent("Create Pickup", "Also create a prefab for the item lying in the world, with a collider and Rigidbody."),
                _args.CreatePickup);

            DrawFolderField();
        }

        void DrawFolderField()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Folder");
            EditorGUILayout.SelectableLabel(_args.Folder, EditorStyles.textField, GUILayout.Height(EditorGUIUtility.singleLineHeight));

            if (GUILayout.Button("...", GUILayout.Width(28)))
            {
                var selected = EditorUtility.OpenFolderPanel("Item Folder", _args.Folder, string.Empty);
                var projectPath = ToProjectPath(selected);

                if (projectPath != null)
                    _args.Folder = projectPath;
                else if (!string.IsNullOrEmpty(selected))
                    ShowNotification(new GUIContent("Choose a folder inside this project's Assets folder."));
            }

            EditorGUILayout.EndHorizontal();
        }

        void DrawCreate()
        {
            var errors = CreateItem.Validate(_args);

            foreach (var error in errors)
                EditorGUILayout.HelpBox(error, MessageType.Info);

            if (errors.Count == 0)
            {
                var files = CreateItem.ItemPath(_args) + "\n" + CreateItem.PrefabPath(_args);
                if (_args.CreatePickup)
                    files += "\n" + CreateItem.PickupPath(_args);

                EditorGUILayout.HelpBox("Creates:\n" + files, MessageType.None);
            }

            using (new EditorGUI.DisabledScope(errors.Count > 0))
            {
                if (GUILayout.Button("Create Item", GUILayout.Height(28)))
                    Create();
            }
        }

        void Create()
        {
            var result = CreateItem.Create(_args);

            foreach (var warning in result.Warnings)
                Debug.LogWarning(warning, result.Item);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    Debug.LogError(error);

                ShowNotification(new GUIContent("Item not created. See the Console."));
                return;
            }

            Debug.Log($"Created item '{result.Item.name}'.", result.Item);
            Selection.activeObject = result.Item;
            EditorGUIUtility.PingObject(result.Item);
            ShowNotification(new GUIContent($"Created '{result.Item.name}'."));

            // Ready to pose straight away.
            SwitchTab(Tab.Pose);
            LoadPoseItem(result.Item);

            _args.Name = string.Empty;
            _args.Model = null;
        }

        static string GetSelectedFolder()
        {
            foreach (var guid in Selection.assetGUIDs)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                if (AssetDatabase.IsValidFolder(path))
                    return path;

                var directory = Path.GetDirectoryName(path)?.Replace('\\', '/');
                if (!string.IsNullOrEmpty(directory) && AssetDatabase.IsValidFolder(directory))
                    return directory;
            }

            return "Assets";
        }

        static string ToProjectPath(string absolutePath)
        {
            if (string.IsNullOrEmpty(absolutePath))
                return null;

            var assetsPath = Application.dataPath.Replace('\\', '/');
            absolutePath = absolutePath.Replace('\\', '/');

            if (absolutePath == assetsPath)
                return "Assets";

            if (absolutePath.StartsWith(assetsPath + "/"))
                return "Assets" + absolutePath.Substring(assetsPath.Length);

            return null;
        }
    }
}
