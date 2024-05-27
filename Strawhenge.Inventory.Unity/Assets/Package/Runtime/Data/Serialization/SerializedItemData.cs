using FunctionalUtilities;
using Strawhenge.Inventory.Unity.Items.Consumables;
using Strawhenge.Inventory.Unity.Monobehaviours;
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

        [FormerlySerializedAs("LeftHandHoldData"), SerializeField]
        SerializedHoldItemData _leftHandHoldData;

        [FormerlySerializedAs("RightHandHoldData"), SerializeField]
        SerializedHoldItemData _rightHandHoldData;

        [FormerlySerializedAs("HolsterItemData"), SerializeField]
        SerializedHolsterItemData[] _holsterItemData;

        string IItemData.Name => _name;

        ItemScript IItemData.Prefab => _prefab;

        ItemSize IItemData.Size => _size;

        IHoldItemData IItemData.LeftHandHoldData => _leftHandHoldData;

        IHoldItemData IItemData.RightHandHoldData => _rightHandHoldData;

        IEnumerable<IHolsterItemData> IItemData.HolsterItemData => _holsterItemData;

        Maybe<IConsumableData> IItemData.ConsumableData => Maybe.None<IConsumableData>();
    }
}