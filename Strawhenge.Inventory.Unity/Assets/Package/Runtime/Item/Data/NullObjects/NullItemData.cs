using FunctionalUtilities;
using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items.Data
{
    public class NullItemData : IItemData
    {
        public static IItemData Instance { get; } = new NullItemData();

        static readonly Lazy<ItemScript> PrefabLazy = new(() =>
            new GameObject(nameof(NullItemData)).AddComponent<ItemScript>());

        NullItemData()
        {
        }

        public ItemScript Prefab => PrefabLazy.Value;

        public Maybe<ItemPickupScript> PickupPrefab { get; } = Maybe.None<ItemPickupScript>();

        public IHoldItemData LeftHandHoldData => NullHoldItemData.Instance;

        public IHoldItemData RightHandHoldData => NullHoldItemData.Instance;

        public IDrawAnimationSettings DrawAnimationSettings => NullDrawAnimationSettings.Instance;
    }
}