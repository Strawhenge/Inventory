using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Unity.Consumables;
using System.Collections.Generic;

namespace Strawhenge.Inventory.Unity.Items.Data
{
    public interface IItemData
    {
        ItemScript Prefab { get; }

        Maybe<ItemPickupScript> PickupPrefab { get; }

        IHoldItemData LeftHandHoldData { get; }

        IHoldItemData RightHandHoldData { get; }
    }
}