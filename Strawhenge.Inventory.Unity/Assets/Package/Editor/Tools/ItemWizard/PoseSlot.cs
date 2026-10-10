using Strawhenge.Inventory.Unity.Items;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Editor.Tools.ItemWizard
{
    enum PoseSlotKind
    {
        LeftHand,
        RightHand,
        Holster
    }

    /// <summary>
    /// A place on a character where an item can be posed: a hand or a holster.
    /// </summary>
    class PoseSlot
    {
        PoseSlot(PoseSlotKind kind, string displayName, Transform point, HolsterScriptableObject holster = null)
        {
            Kind = kind;
            DisplayName = displayName;
            Point = point;
            Holster = holster;
        }

        public PoseSlotKind Kind { get; }

        public string DisplayName { get; }

        public Transform Point { get; }

        public HolsterScriptableObject Holster { get; }

        public bool IsHand => Kind != PoseSlotKind.Holster;

        public static IReadOnlyList<PoseSlot> FindSlots(InventoryScript character)
        {
            var slots = new List<PoseSlot>();
            if (character == null)
                return slots;

            var leftHand = character.GetComponentInChildren<LeftHandScript>(includeInactive: true);
            if (leftHand != null)
                slots.Add(new PoseSlot(PoseSlotKind.LeftHand, "Left Hand", leftHand.transform));

            var rightHand = character.GetComponentInChildren<RightHandScript>(includeInactive: true);
            if (rightHand != null)
                slots.Add(new PoseSlot(PoseSlotKind.RightHand, "Right Hand", rightHand.transform));

            var holsters = character
                .GetComponentsInChildren<HolsterScript>(includeInactive: true)
                .Select(script => (script, holster: GetHolsterAsset(script)))
                .Where(x => x.holster != null)
                .OrderBy(x => x.holster.name);

            foreach (var (script, holster) in holsters)
                slots.Add(new PoseSlot(PoseSlotKind.Holster, $"Holster: {holster.name}", script.transform, holster));

            return slots;
        }

        static HolsterScriptableObject GetHolsterAsset(HolsterScript holsterScript)
        {
            // The holster asset is private on HolsterScript, and HolsterName throws when it's unset.
            var property = new SerializedObject(holsterScript).FindProperty(HolsterScript.HolsterFieldName);
            return property?.objectReferenceValue as HolsterScriptableObject;
        }
    }
}
