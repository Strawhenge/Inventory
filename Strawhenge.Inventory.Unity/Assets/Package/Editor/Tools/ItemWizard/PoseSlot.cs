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

        /// <summary>
        /// Finds the character's slots the way InventoryScript does at runtime: the hands and holsters assigned
        /// on it, falling back to searching only for unassigned hands. Adds a warning for anything the game
        /// wouldn't find.
        /// </summary>
        public static IReadOnlyList<PoseSlot> FindSlots(InventoryScript character, List<string> warnings)
        {
            var slots = new List<PoseSlot>();
            if (character == null)
                return slots;

            var leftHand = Resolve(character, character.LeftHand, "Left Hand", warnings);
            if (leftHand != null)
                slots.Add(new PoseSlot(PoseSlotKind.LeftHand, "Left Hand", leftHand.transform));

            var rightHand = Resolve(character, character.RightHand, "Right Hand", warnings);
            if (rightHand != null)
                slots.Add(new PoseSlot(PoseSlotKind.RightHand, "Right Hand", rightHand.transform));

            // Only holsters in InventoryScript's Holsters list are used at runtime, so only those are offered.
            var holsters = character.Holsters
                .Where(script => script != null)
                .Select(script => (script, holster: GetHolsterAsset(script)))
                .Where(x => x.holster != null)
                .GroupBy(x => x.holster)
                .Select(group => group.First())
                .OrderBy(x => x.holster.name);

            foreach (var (script, holster) in holsters)
                slots.Add(new PoseSlot(PoseSlotKind.Holster, $"Holster: {holster.name}", script.transform, holster));

            return slots;
        }

        /// <summary>
        /// Finds the character's Animator the same way as its hands.
        /// </summary>
        public static Animator FindAnimator(InventoryScript character, List<string> warnings = null) =>
            character == null ? null : Resolve(character, character.Animator, "Animator", warnings);

        /// <summary>
        /// The assigned reference if set. Otherwise the runtime's fallback (below InventoryScript), and failing
        /// that a search of the whole character, with a warning because the game wouldn't find it there.
        /// </summary>
        static T Resolve<T>(InventoryScript character, T assigned, string label, List<string> warnings)
            where T : Component
        {
            if (assigned != null)
                return assigned;

            var found = character.GetComponentInChildren<T>(includeInactive: true);
            if (found != null)
                return found;

            found = CharacterRoot(character).GetComponentInChildren<T>(includeInactive: true);
            if (found != null)
            {
                warnings?.Add($"{label} isn't assigned on InventoryScript and isn't below it, so the game won't " +
                              $"find '{found.name}'. Assign it in InventoryScript's Inspector.");
            }

            return found;
        }

        /// <summary>
        /// The character the InventoryScript belongs to: the Animator's object if the InventoryScript is below it,
        /// otherwise the InventoryScript's own object. Deliberately not transform.root, which may be a scene-level
        /// parent holding several characters.
        /// </summary>
        static Transform CharacterRoot(InventoryScript character)
        {
            var animator = character.Animator != null
                ? character.Animator
                : character.GetComponentInParent<Animator>();

            return animator != null && character.transform.IsChildOf(animator.transform)
                ? animator.transform
                : character.transform;
        }

        static HolsterScriptableObject GetHolsterAsset(HolsterScript holsterScript)
        {
            // The holster asset is private on HolsterScript, and HolsterName throws when it's unset.
            var property = new SerializedObject(holsterScript).FindProperty(HolsterScript.HolsterFieldName);
            return property?.objectReferenceValue as HolsterScriptableObject;
        }
    }
}
