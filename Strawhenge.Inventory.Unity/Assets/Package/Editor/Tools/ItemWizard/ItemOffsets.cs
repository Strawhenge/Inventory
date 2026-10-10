using Strawhenge.Inventory.Unity.Items.ItemData;
using UnityEditor;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Editor.Tools.ItemWizard
{
    /// <summary>
    /// Reads and writes an item's position and rotation offsets for a hand or holster.
    /// </summary>
    static class ItemOffsets
    {
        const string LeftHand = "_leftHandHoldItemData";
        const string RightHand = "_rightHandHoldItemData";
        const string Holsters = "_holsterItemData";
        const string Holster = "_holster";
        const string Position = "_positionOffset";
        const string Rotation = "_rotationOffset";

        public static bool TryRead(ItemScriptableObject item, PoseSlot slot, out Vector3 position, out Vector3 rotation)
        {
            position = Vector3.zero;
            rotation = Vector3.zero;

            var data = FindData(new SerializedObject(item), slot);
            if (data == null)
                return false;

            position = data.FindPropertyRelative(Position).vector3Value;
            rotation = data.FindPropertyRelative(Rotation).vector3Value;
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
            var data = FindData(serializedItem, slot) ?? AddHolster(serializedItem, slot);

            data.FindPropertyRelative(Position).vector3Value = position;
            data.FindPropertyRelative(Rotation).vector3Value = rotation;
            serializedItem.ApplyModifiedProperties();
        }

        public static void CopyHand(ItemScriptableObject item, PoseSlotKind from, PoseSlotKind to)
        {
            var serializedItem = new SerializedObject(item);
            var source = serializedItem.FindProperty(HandPath(from));
            var target = serializedItem.FindProperty(HandPath(to));

            target.FindPropertyRelative(Position).vector3Value = source.FindPropertyRelative(Position).vector3Value;
            target.FindPropertyRelative(Rotation).vector3Value = source.FindPropertyRelative(Rotation).vector3Value;
            serializedItem.ApplyModifiedProperties();
        }

        static SerializedProperty FindData(SerializedObject serializedItem, PoseSlot slot)
        {
            if (slot.IsHand)
                return serializedItem.FindProperty(HandPath(slot.Kind));

            var holsters = serializedItem.FindProperty(Holsters);
            for (var i = 0; i < holsters.arraySize; i++)
            {
                var entry = holsters.GetArrayElementAtIndex(i);
                if (entry.FindPropertyRelative(Holster).objectReferenceValue == slot.Holster)
                    return entry;
            }

            return null;
        }

        static SerializedProperty AddHolster(SerializedObject serializedItem, PoseSlot slot)
        {
            var holsters = serializedItem.FindProperty(Holsters);
            holsters.arraySize++;

            // A new array element copies the previous one, so reset everything except the holster.
            var entry = holsters.GetArrayElementAtIndex(holsters.arraySize - 1);
            ResetChildren(entry);
            entry.FindPropertyRelative(Holster).objectReferenceValue = slot.Holster;
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

        static string HandPath(PoseSlotKind kind) => kind == PoseSlotKind.LeftHand ? LeftHand : RightHand;
    }
}
