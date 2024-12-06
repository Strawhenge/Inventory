using FunctionalUtilities;
using Strawhenge.Common.Unity.Serialization;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.Consumables;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Strawhenge.Inventory.Unity.Data
{
    [Serializable]
    public class SerializedItemData : IItemData
    {
        [FormerlySerializedAs("Name"), SerializeField]
        string _name;

        [FormerlySerializedAs("Prefab"), SerializeField]
        ItemScript _prefab;

        [FormerlySerializedAs("Size"), SerializeField]
        ItemSize _size;

        [SerializeField] bool _isStorable;
        [SerializeField] int _weight;

        [FormerlySerializedAs("LeftHandHoldData"), SerializeField]
        SerializedHoldItemData _leftHandHoldData;

        [FormerlySerializedAs("RightHandHoldData"), SerializeField]
        SerializedHoldItemData _rightHandHoldData;

        [FormerlySerializedAs("HolsterItemData"), SerializeField]
        SerializedHolsterItemData[] _holsterItemData;

        [SerializeField]
        SerializedSource<IConsumableData, SerializedConsumableData, ConsumableDataScriptableObject> _consumable;

        string IItemData.Name => _name;

        ItemScript IItemData.Prefab => _prefab;

        public Maybe<ItemPickupScript> PickupPrefab => Maybe.None<ItemPickupScript>();

        ItemSize IItemData.Size => _size;

        bool IItemData.IsStorable => _isStorable;

        int IItemData.Weight => _weight;

        IHoldItemData IItemData.LeftHandHoldData => _leftHandHoldData;

        IHoldItemData IItemData.RightHandHoldData => _rightHandHoldData;

        IEnumerable<IHolsterItemData> IItemData.HolsterItemData => _holsterItemData;

        Maybe<IConsumableData> IItemData.ConsumableData => _consumable.TryGetValue(out var consumable)
            ? Maybe.Some(consumable)
            : Maybe.None<IConsumableData>();
    }
}