using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.HoldItemData;
using Strawhenge.Inventory.Unity.Items.HolsterItemData;
using Strawhenge.Inventory.Unity.Items.ItemData;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Editor.Tools.ItemWizard
{
    /// <summary>
    /// Reads and writes an item's position and rotation offsets for a hand or holster.
    /// </summary>
    static class ItemOffsets
    {
        public static bool TryRead(ItemScriptableObject item, PoseSlot slot, out Vector3 position, out Vector3 rotation)
        {
            position = Vector3.zero;
            rotation = Vector3.zero;

            var data = FindData(new SerializedObject(item), slot);
            if (data == null)
                return false;

            position = data.FindPropertyRelative(PositionName(slot)).vector3Value;
            rotation = data.FindPropertyRelative(RotationName(slot)).vector3Value;
            return true;
        }

        public static bool HasData(ItemScriptableObject item, PoseSlot slot) =>
            FindData(new SerializedObject(item), slot) != null;

        /// <summary>
        /// Writes the offsets. For a holster the item doesn't fit yet, a holster entry is added.
        /// Recorded for undo.
        /// </summary>
        public static void Write(ItemScriptableObject item, PoseSlot slot, Vector3 position, Vector3 rotation)
        {
            var serializedItem = new SerializedObject(item);
            var data = FindData(serializedItem, slot) ?? AddHolsterEntry(serializedItem, slot.Holster);

            data.FindPropertyRelative(PositionName(slot)).vector3Value = position;
            data.FindPropertyRelative(RotationName(slot)).vector3Value = rotation;
            serializedItem.ApplyModifiedProperties();
        }

        public static void CopyHand(ItemScriptableObject item, PoseSlotKind from, PoseSlotKind to)
        {
            var serializedItem = new SerializedObject(item);
            var source = serializedItem.FindProperty(HandPath(from));
            var target = serializedItem.FindProperty(HandPath(to));

            var position = SerializedHoldItemData.PositionOffsetFieldName;
            var rotation = SerializedHoldItemData.RotationOffsetFieldName;

            target.FindPropertyRelative(position).vector3Value = source.FindPropertyRelative(position).vector3Value;
            target.FindPropertyRelative(rotation).vector3Value = source.FindPropertyRelative(rotation).vector3Value;
            serializedItem.ApplyModifiedProperties();
        }

        /// <summary>
        /// The holsters the item fits, in order. An entry may be null if its holster isn't set.
        /// </summary>
        public static IReadOnlyList<HolsterScriptableObject> GetHolsters(ItemScriptableObject item)
        {
            var holsters = new SerializedObject(item).FindProperty(ItemScriptableObject.HolsterItemDataFieldName);
            var result = new List<HolsterScriptableObject>(holsters.arraySize);

            for (var i = 0; i < holsters.arraySize; i++)
            {
                var holster = holsters.GetArrayElementAtIndex(i)
                    .FindPropertyRelative(SerializedHolsterItemData.HolsterFieldName)
                    .objectReferenceValue as HolsterScriptableObject;

                result.Add(holster);
            }

            return result;
        }

        /// <summary>
        /// Adds a holster entry with zero offsets, unless the item already fits that holster. Recorded for undo.
        /// </summary>
        public static bool AddHolster(ItemScriptableObject item, HolsterScriptableObject holster)
        {
            if (holster == null || GetHolsters(item).Contains(holster))
                return false;

            var serializedItem = new SerializedObject(item);
            AddHolsterEntry(serializedItem, holster);
            serializedItem.ApplyModifiedProperties();
            return true;
        }

        /// <summary>
        /// Removes the holster entry at the index from <see cref="GetHolsters"/>. Recorded for undo.
        /// </summary>
        public static void RemoveHolster(ItemScriptableObject item, int index)
        {
            var serializedItem = new SerializedObject(item);
            var holsters = serializedItem.FindProperty(ItemScriptableObject.HolsterItemDataFieldName);

            if (index < 0 || index >= holsters.arraySize)
                return;

            holsters.DeleteArrayElementAtIndex(index);
            serializedItem.ApplyModifiedProperties();
        }

        static SerializedProperty FindData(SerializedObject serializedItem, PoseSlot slot)
        {
            if (slot.IsHand)
                return serializedItem.FindProperty(HandPath(slot.Kind));

            var holsters = serializedItem.FindProperty(ItemScriptableObject.HolsterItemDataFieldName);
            for (var i = 0; i < holsters.arraySize; i++)
            {
                var entry = holsters.GetArrayElementAtIndex(i);
                if (entry.FindPropertyRelative(SerializedHolsterItemData.HolsterFieldName).objectReferenceValue == slot.Holster)
                    return entry;
            }

            return null;
        }

        static SerializedProperty AddHolsterEntry(SerializedObject serializedItem, HolsterScriptableObject holster)
        {
            var holsters = serializedItem.FindProperty(ItemScriptableObject.HolsterItemDataFieldName);
            holsters.arraySize++;

            // A new array element copies the previous one, so reset everything except the holster.
            var entry = holsters.GetArrayElementAtIndex(holsters.arraySize - 1);
            ResetChildren(entry);
            entry.FindPropertyRelative(SerializedHolsterItemData.HolsterFieldName).objectReferenceValue = holster;
            return entry;
        }

        static void ResetChildren(SerializedProperty property)
        {
            var iterator = property.Copy();
            var end = property.GetEndProperty();

            if (!iterator.NextVisible(enterChildren: true))
                return;

            while (!SerializedProperty.EqualContents(iterator, end))
            {
                switch (iterator.propertyType)
                {
                    case SerializedPropertyType.ObjectReference:
                        iterator.objectReferenceValue = null;
                        break;
                    case SerializedPropertyType.Vector3:
                        iterator.vector3Value = Vector3.zero;
                        break;
                    case SerializedPropertyType.Enum:
                    case SerializedPropertyType.Integer:
                        iterator.intValue = 0;
                        break;
                }

                if (!iterator.NextVisible(enterChildren: true))
                    break;
            }
        }

        static string HandPath(PoseSlotKind kind) =>
            kind == PoseSlotKind.LeftHand
                ? ItemScriptableObject.LeftHandHoldItemDataFieldName
                : ItemScriptableObject.RightHandHoldItemDataFieldName;

        // Hand offsets live on SerializedHoldItemData and holster offsets on SerializedHolsterItemData.
        static string PositionName(PoseSlot slot) =>
            slot.IsHand
                ? SerializedHoldItemData.PositionOffsetFieldName
                : SerializedHolsterItemData.PositionOffsetFieldName;

        static string RotationName(PoseSlot slot) =>
            slot.IsHand
                ? SerializedHoldItemData.RotationOffsetFieldName
                : SerializedHolsterItemData.RotationOffsetFieldName;
    }
}
