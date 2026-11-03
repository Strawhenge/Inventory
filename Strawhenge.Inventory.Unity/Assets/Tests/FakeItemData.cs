using FunctionalUtilities;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.DrawAnimationSettings;
using Strawhenge.Inventory.Unity.Items.HoldItemData;
using Strawhenge.Inventory.Unity.Items.ItemData;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Tests
{
    /// <summary>
    /// Custom data that claims to be item data, to check it can't replace the item's own.
    /// </summary>
    public class FakeItemData : ScriptableObject, IItemData
    {
        public ItemScript Prefab => null;

        public Maybe<ItemPickupScript> PickupPrefab => Maybe.None<ItemPickupScript>();

        public IHoldItemData LeftHandHoldData => NullHoldItemData.Instance;

        public IHoldItemData RightHandHoldData => NullHoldItemData.Instance;

        public IDrawAnimationSettings DrawAnimationSettings => NullDrawAnimationSettings.Instance;
    }
}
