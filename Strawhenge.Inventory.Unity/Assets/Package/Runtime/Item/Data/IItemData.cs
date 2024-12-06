using FunctionalUtilities;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Consumables;
using System.Collections.Generic;

namespace Strawhenge.Inventory.Unity.Items.Data
{
    public interface IItemData
    {
        string Name { get; }

        ItemScript Prefab { get; }

        Maybe<ItemPickupScript> PickupPrefab { get; }

        ItemSize Size { get; }

        bool IsStorable { get; }

        int Weight { get; }

        IHoldItemData LeftHandHoldData { get; }

        IHoldItemData RightHandHoldData { get; }

        IEnumerable<IHolsterItemData> HolsterItemData { get; }

        Maybe<IConsumableData> ConsumableData { get; }
    }
}