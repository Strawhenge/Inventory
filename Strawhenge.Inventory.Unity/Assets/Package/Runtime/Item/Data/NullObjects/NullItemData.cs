using FunctionalUtilities;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Consumables;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items.Data
{
    public class NullItemData : IItemData
    {
        public string Name => string.Empty;

        public ItemScript Prefab => new GameObject(nameof(NullItemData)).AddComponent<ItemScript>();

        public Maybe<ItemPickupScript> PickupPrefab => Maybe.None<ItemPickupScript>();

        public ItemSize Size => ItemSize.OneHanded;

        public bool IsStorable => false;

        public int Weight => 0;

        public IHoldItemData LeftHandHoldData => new NullHoldItemData();

        public IHoldItemData RightHandHoldData => new NullHoldItemData();

        public IEnumerable<IHolsterItemData> HolsterItemData => Enumerable.Empty<IHolsterItemData>();

        public Maybe<IConsumableData> ConsumableData => Maybe.None<IConsumableData>();
    }
}