using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.ItemData;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Editor.Tools.ItemWizard
{
    [System.Serializable]
    public class CreateItemArgs
    {
        public string Name;
        public GameObject Model;
        public ItemSize Size = ItemSize.OneHanded;
        public bool IsStorable = true;
        public int Weight = 1;
        public bool CreatePickup = true;
        public string Folder = "Assets";
    }

    public class CreateItemResult
    {
        public ItemScriptableObject Item;
        public ItemScript Prefab;
        public ItemPickupScript PickupPrefab;
        public readonly List<string> Errors = new();
        public readonly List<string> Warnings = new();

        public bool Succeeded => Errors.Count == 0 && Item != null;
    }

    public static class CreateItem
    {
        public const string PickupSuffix = " Pickup";

        public static IReadOnlyList<string> Validate(CreateItemArgs args)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(args.Name))
                errors.Add("Enter a name.");
            else if (args.Name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                errors.Add("The name contains characters that can't be used in a file name.");

            if (args.Model == null)
                errors.Add("Choose a model or prefab.");
            else if (!EditorUtility.IsPersistent(args.Model))
                errors.Add("The model must be an asset (a model file or prefab), not an object in the scene.");

            if (args.Weight < 0)
                errors.Add("Weight can't be negative.");

            if (string.IsNullOrWhiteSpace(args.Folder) || !AssetDatabase.IsValidFolder(args.Folder))
                errors.Add("Choose a folder inside the project.");

            if (errors.Count == 0)
            {
                errors.AddRange(GetPaths(args)
                    .Where(path => AssetDatabase.LoadAssetAtPath<Object>(path) != null)
                    .Select(path => $"'{path}' already exists."));
            }

            return errors;
        }

        public static CreateItemResult Create(CreateItemArgs args)
        {
            var result = new CreateItemResult();
            result.Errors.AddRange(Validate(args));
            if (result.Errors.Count > 0)
                return result;

            var name = args.Name.Trim();
            var itemPath = ItemPath(args);
            var prefabPath = PrefabPath(args);
            var pickupPath = PickupPath(args);

            if (args.Model.GetComponentsInChildren<Rigidbody>(includeInactive: true).Any())
                result.Warnings.Add($"'{args.Model.name}' contains a Rigidbody. The held item may fall out of the hand; " +
                                    "consider removing it from the held prefab.");

            // The item asset is created first so the pickup prefab can reference it.
            var item = ScriptableObject.CreateInstance<ItemScriptableObject>();
            AssetDatabase.CreateAsset(item, itemPath);

            var prefab = CreateHeldPrefab(name, args.Model, prefabPath);

            ItemPickupScript pickup = null;
            if (args.CreatePickup)
                pickup = CreatePickupPrefab(name, args.Model, item, pickupPath, result);

            var serializedItem = new SerializedObject(item);
            serializedItem.FindProperty(ItemScriptableObject.PrefabFieldName).objectReferenceValue = prefab;
            serializedItem.FindProperty(ItemScriptableObject.PickupPrefabFieldName).objectReferenceValue = pickup;
            serializedItem.FindProperty(ItemScriptableObject.SizeFieldName).intValue = (int)args.Size;
            serializedItem.FindProperty(ItemScriptableObject.IsStorableFieldName).boolValue = args.IsStorable;
            serializedItem.FindProperty(ItemScriptableObject.WeightFieldName).intValue = Mathf.Max(0, args.Weight);
            serializedItem.ApplyModifiedPropertiesWithoutUndo();

            EditorUtility.SetDirty(item);
            AssetDatabase.SaveAssets();

            result.Item = item;
            result.Prefab = prefab;
            result.PickupPrefab = pickup;
            return result;
        }

        public static string ItemPath(CreateItemArgs args) => $"{args.Folder}/{args.Name.Trim()}.asset";

        public static string PrefabPath(CreateItemArgs args) => $"{args.Folder}/{args.Name.Trim()}.prefab";

        public static string PickupPath(CreateItemArgs args) => $"{args.Folder}/{args.Name.Trim()}{PickupSuffix}.prefab";

        static IEnumerable<string> GetPaths(CreateItemArgs args)
        {
            yield return ItemPath(args);
            yield return PrefabPath(args);

            if (args.CreatePickup)
                yield return PickupPath(args);
        }

        static ItemScript CreateHeldPrefab(string name, GameObject model, string path)
        {
            var root = new GameObject(name);
            try
            {
                root.AddComponent<ItemScript>();
                AddModel(root, model);

                var saved = PrefabUtility.SaveAsPrefabAsset(root, path);
                return saved.GetComponent<ItemScript>();
            }
            finally
            {
                Object.DestroyImmediate(root);
            }
        }

        static ItemPickupScript CreatePickupPrefab(
            string name,
            GameObject model,
            ItemScriptableObject item,
            string path,
            CreateItemResult result)
        {
            var root = new GameObject(name + PickupSuffix);
            try
            {
                AddModel(root, model);

                var collider = root.AddComponent<BoxCollider>();
                if (TryGetLocalBounds(root, out var bounds))
                {
                    collider.center = bounds.center;
                    collider.size = bounds.size;
                }
                else
                {
                    collider.size = Vector3.one * 0.1f;
                    result.Warnings.Add($"'{model.name}' has no renderers, so the pickup collider was given a default " +
                                        "size. Resize it on the pickup prefab.");
                }

                root.AddComponent<Rigidbody>();

                var pickup = root.AddComponent<ItemPickupScript>();
                var serializedPickup = new SerializedObject(pickup);
                serializedPickup.FindProperty(ItemPickupScript.DataFieldName).objectReferenceValue = item;
                serializedPickup.ApplyModifiedPropertiesWithoutUndo();

                var saved = PrefabUtility.SaveAsPrefabAsset(root, path);
                return saved.GetComponent<ItemPickupScript>();
            }
            finally
            {
                Object.DestroyImmediate(root);
            }
        }

        static void AddModel(GameObject root, GameObject model)
        {
            // Keeps the link to the source model/prefab, so changes to it flow through to the item.
            var instance = (GameObject)PrefabUtility.InstantiatePrefab(model);
            instance.transform.SetParent(root.transform, worldPositionStays: false);
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = Quaternion.identity;
        }

        static bool TryGetLocalBounds(GameObject root, out Bounds bounds)
        {
            // The root sits at the origin with no rotation or scale, so world bounds are local bounds.
            var renderers = root.GetComponentsInChildren<Renderer>(includeInactive: true);
            bounds = default;

            if (renderers.Length == 0)
                return false;

            bounds = renderers[0].bounds;
            foreach (var renderer in renderers.Skip(1))
                bounds.Encapsulate(renderer.bounds);

            return bounds.size != Vector3.zero;
        }
    }
}
