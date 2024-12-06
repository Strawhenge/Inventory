using FunctionalUtilities;
using Strawhenge.Common.Unity.Serialization;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Consumables;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Strawhenge.Inventory.Unity.Items.Data.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Strawhenge/Inventory/Item")]
    public class ItemScriptableObject : ScriptableObject, IItemData
    {
        [FormerlySerializedAs("prefab"), SerializeField]
        ItemScript _prefab;

        [SerializeField, Tooltip("Optional.")] ItemPickupScript _pickupPrefab;

        [FormerlySerializedAs("size"), SerializeField]
        ItemSize _size;

        [SerializeField] bool _isStorable;
        [SerializeField] int _weight;

        [FormerlySerializedAs("leftHandHoldItemData"), SerializeField]
        SerializedHoldItemData _leftHandHoldItemData;

        [FormerlySerializedAs("rightHandHoldItemData"), SerializeField]
        SerializedHoldItemData _rightHandHoldItemData;

        [FormerlySerializedAs("holsterItemData"), SerializeField]
        SerializedHolsterItemData[] _holsterItemData;

        [SerializeField]
        SerializedSource<IConsumableData, SerializedConsumableData, ConsumableDataScriptableObject> _consumable;

        string IItemData.Name => name;

        ItemScript IItemData.Prefab => _prefab;

        Maybe<ItemPickupScript> IItemData.PickupPrefab => _pickupPrefab == null
            ? Maybe.None<ItemPickupScript>()
            : Maybe.Some(_pickupPrefab);

        ItemSize IItemData.Size => _size;

        bool IItemData.IsStorable => _isStorable;

        int IItemData.Weight => _weight;

        IHoldItemData IItemData.LeftHandHoldData => _leftHandHoldItemData;

        IHoldItemData IItemData.RightHandHoldData => _rightHandHoldItemData;

        IEnumerable<IHolsterItemData> IItemData.HolsterItemData => _holsterItemData;

        Maybe<IConsumableData> IItemData.ConsumableData => _consumable.TryGetValue(out var consumable)
            ? Maybe.Some(consumable)
            : Maybe.None<IConsumableData>();
    }
}