using System;
using System.Collections.Generic;
using System.Linq;
using FunctionalUtilities;
using Strawhenge.Inventory.Effects;

namespace Strawhenge.Inventory.Items
{
    public class ItemDataBuilder
    {
        public static ItemDataBuilder Create(
            string name,
            ItemSize size,
            bool isStorable,
            int weight,
            Action<IDataSetter> setData) =>
            new ItemDataBuilder(name, size, isStorable, weight, setData);

        readonly string _name;
        readonly ItemSize _size;
        readonly bool _isStorable;
        readonly int _weight;
        readonly Action<IDataSetter> _setData;

        readonly List<(string name, Action<IDataSetter> setData)> _holsters =
            new List<(string name, Action<IDataSetter> setData)>();

        (EffectData[] effects, Action<IDataSetter> setData)? _consumable;

        ItemDataBuilder(string name, ItemSize size, bool isStorable, int weight, Action<IDataSetter> setData)
        {
            _name = name;
            _size = size;
            _isStorable = isStorable;
            _weight = weight;
            _setData = setData;
        }

        public void AddHolster(string name, Action<IDataSetter> setData) =>
            _holsters.Add((name, setData));

        public void SetConsumable(IEnumerable<EffectData> effects, Action<IDataSetter> setData) =>
            _consumable = (effects.ToArray(), setData);

        public ItemData Build()
        {
            var genericData = new GenericData();
            _setData(genericData);

            var holsters = _holsters.Select(x =>
            {
                var genericHolsterData = new GenericData();
                x.setData(genericHolsterData);
                return new HolsterItemData(x.name, genericHolsterData);
            });

            var consumable = Maybe.None<ConsumableItemData>();
            if (_consumable.HasValue)
            {
                var genericConsumableData = new GenericData();
                _consumable.Value.setData(genericConsumableData);
                consumable = new ConsumableItemData(_consumable.Value.effects, genericConsumableData);
            }

            return new ItemData(_name, _size, _isStorable, _weight, consumable, holsters, genericData);
        }
    }
}