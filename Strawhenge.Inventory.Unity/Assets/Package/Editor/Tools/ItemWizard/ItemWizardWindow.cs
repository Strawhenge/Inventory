using Strawhenge.Inventory.Items;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Editor.Tools.ItemWizard
{
    public class ItemWizardWindow : EditorWindow
    {
        const string Title = "Item Wizard";

        [MenuItem("Strawhenge/Inventory/Create Item...", priority = 1)]
        public static void Open()
        {
            var window = GetWindow<ItemWizardWindow>(utility: false, title: Title, focus: true);
            window.minSize = new Vector2(360, 260);
            window._args.Folder = GetSelectedFolder();
        }

        [MenuItem("Assets/Create/Strawhenge/Inventory/Item (Wizard)...", priority = 0)]
        static void OpenFromAssetsMenu() => Open();

        [SerializeField] CreateItemArgs _args = new();
        Vector2 _scroll;

        void OnGUI()
        {
            _scroll = EditorGUILayout.BeginScrollView(_scroll);

            DrawBasics();
            EditorGUILayout.Space();
            DrawCreate();

            EditorGUILayout.EndScrollView();
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
