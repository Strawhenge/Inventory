using System;
using System.Collections.Generic;
using System.Linq;
using FunctionalUtilities;
using Strawhenge.Inventory.Effects;

namespace Strawhenge.Inventory.Items
{
    public class ItemBuilder
    {
        public static ItemBuilder Create(
            string name,
            ItemSize size,
            bool isStorable,
            int weight,
            Action<IDataSetter> setData = null) =>
            new ItemBuilder(name, size, isStorable, weight, setData ?? (_ =>
            {
            }));

        readonly string _name;
        readonly ItemSize _size;
        readonly bool _isStorable;
        readonly int _weight;
        readonly Action<IDataSetter> _setData;

        readonly List<(string name, Action<IDataSetter> setData)> _holsters =
            new List<(string name, Action<IDataSetter> setData)>();

        (EffectData[] effects, Action<IDataSetter> setData)? _consumable;

        ItemBuilder(string name, ItemSize size, bool isStorable, int weight, Action<IDataSetter> setData)
        {
            _name = name;
            _size = size;
            _isStorable = isStorable;
            _weight = weight;
            _setData = setData;
        }

        public ItemBuilder AddHolster(string name, Action<IDataSetter> setData = null)
        {
            setData = setData ?? (_ =>
            {
            });

            _holsters.Add((name, setData));
            return this;
        }

        public ItemBuilder SetConsumable(IEnumerable<EffectData> effects, Action<IDataSetter> setData = null)
        {
            setData = setData ?? (_ =>
            {
            });

            _consumable = (effects.ToArray(), setData);
            return this;
        }

        public Item Build()
        {
            var genericData = new GenericData();
            _setData(genericData);

            var holsters = _holsters.Select(x =>
            {
                var genericHolsterData = new GenericData();
                x.setData(genericHolsterData);
                return new ItemHolster(x.name, genericHolsterData);
            });

            var consumable = Maybe.None<ItemConsumable>();
            if (_consumable.HasValue)
            {
                var genericConsumableData = new GenericData();
                _consumable.Value.setData(genericConsumableData);
                consumable = new ItemConsumable(_consumable.Value.effects, genericConsumableData);
            }

            return new Item(_name, _size, _isStorable, _weight, consumable, holsters, genericData);
        }
    }
}