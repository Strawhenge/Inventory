using FunctionalUtilities;
using Strawhenge.Common.Unity.Serialization;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Unity.Consumables;
using UnityEngine;
using UnityEngine.Serialization;

namespace Strawhenge.Inventory.Unity.Items.Data.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Strawhenge/Inventory/Item")]
    public class ItemScriptableObject : ScriptableObject, IItemData
    {
        public ItemData ToItemData()
        {
            var builder = ItemDataBuilder
                .Create(name, _size, _isStorable, _weight, x => x.Set<IItemData>(this));

            foreach (var holsterItemData in _holsterItemData)
            {
                builder.AddHolster(holsterItemData.HolsterName, x => x.Set<IHolsterItemData>(holsterItemData));
            }

            if (_consumable.TryGetValue(out var consumableData))
            {
                builder.SetConsumable(consumableData.Effects, x => x.Set(consumableData));
            }

            return builder.Build();
        }

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
       
        ItemScript IItemData.Prefab => _prefab;

        Maybe<ItemPickupScript> IItemData.PickupPrefab => _pickupPrefab == null
            ? Maybe.None<ItemPickupScript>()
            : Maybe.Some(_pickupPrefab);

        IHoldItemData IItemData.LeftHandHoldData => _leftHandHoldItemData;

        IHoldItemData IItemData.RightHandHoldData => _rightHandHoldItemData;
    }
}