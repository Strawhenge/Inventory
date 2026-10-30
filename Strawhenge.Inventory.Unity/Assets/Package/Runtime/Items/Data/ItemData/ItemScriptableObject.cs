using FunctionalUtilities;
using Strawhenge.Common.Unity.Serialization;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Unity.Items.ConsumableData;
using Strawhenge.Inventory.Unity.Items.DrawAnimationSettings;
using Strawhenge.Inventory.Unity.Items.HoldItemData;
using Strawhenge.Inventory.Unity.Items.HolsterItemData;
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Strawhenge.Inventory.Unity.Items.ItemData
{
    [CreateAssetMenu(menuName = "Strawhenge/Inventory/Item")]
    public sealed partial class ItemScriptableObject : ScriptableObject, IItemData
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

        [SerializeField] SerializedSource<
            IDrawAnimationSettings,
            SerializedDrawAnimationSettings,
            DrawAnimationSettingsScriptableObject> _drawAnimationSettings;

        [FormerlySerializedAs("holsterItemData"), SerializeField]
        SerializedHolsterItemData[] _holsterItemData;

        [SerializeField]
        SerializedSource<IConsumableData, SerializedConsumableData, ConsumableDataScriptableObject> _consumable;

        [SerializeField, Tooltip("Optional. Any scriptable objects to attach to this item. " +
                                 "Read them back with item.GetCustomData<T>().")]
        ScriptableObject[] _customData = Array.Empty<ScriptableObject>();

        ItemScript IItemData.Prefab => _prefab;

        Maybe<ItemPickupScript> IItemData.PickupPrefab => _pickupPrefab == null
            ? Maybe.None<ItemPickupScript>()
            : Maybe.Some(_pickupPrefab);

        IHoldItemData IItemData.LeftHandHoldData => _leftHandHoldItemData;

        IHoldItemData IItemData.RightHandHoldData => _rightHandHoldItemData;

        IDrawAnimationSettings IItemData.DrawAnimationSettings =>
            _drawAnimationSettings.GetValueOrDefault(
                () => NullDrawAnimationSettings.Instance);

        Item _item;

        public Item ToItem()
        {
            if (_item == null)
                _item = BuildItem();

            return _item;
        }

        Item BuildItem()
        {
            var builder = ItemBuilder
                .Create(name, _size, _isStorable, _weight, x =>
                {
                    x.Set<IItemData>(this);
                    x.Set(new CustomItemData(_customData, name, this));
                });

            foreach (var holsterItemData in _holsterItemData)
            {
                builder.AddHolster(
                    holsterItemData.HolsterName,
                    x => x.Set<IHolsterItemData>(holsterItemData));
            }

            if (_consumable.TryGetValue(out var consumableData))
            {
                builder.SetConsumable(
                    consumableData.Effects,
                    x => x.Set(consumableData));
            }

            return builder.Build();
        }
    }
}