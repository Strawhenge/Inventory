using Strawhenge.Inventory.Unity.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Strawhenge.Inventory.Unity
{
    public partial class InventoryScript
    {
        internal static string LeftHandFieldName => nameof(_leftHand);

        internal static string RightHandFieldName => nameof(_rightHand);

        internal static string AnimatorFieldName => nameof(_animator);

        internal static string MaxStoredItemsWeightFieldName => nameof(_maxStoredItemsWeight);

        internal LeftHandScript LeftHand => _leftHand;

        internal RightHandScript RightHand => _rightHand;

        internal Animator Animator => _animator;

        internal IReadOnlyList<HolsterScript> Holsters =>
            _holsters?.Values ?? (IReadOnlyList<HolsterScript>)Array.Empty<HolsterScript>();
    }
}
