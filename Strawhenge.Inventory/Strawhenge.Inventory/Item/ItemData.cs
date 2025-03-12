﻿using System;
using System.Collections.Generic;
using System.Linq;
using FunctionalUtilities;

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

            return new ItemData(_name, _size, _isStorable, _weight, holsters, genericData);
        }
    }

    public interface IDataSetter
    {
        void Set<T>(T value) where T : class;
    }

    class GenericData : IDataSetter
    {
        readonly Dictionary<Type, object> _data = new Dictionary<Type, object>();

        public void Set<T>(T value) where T : class
        {
            _data[typeof(T)] = value;
        }

        public Maybe<T> Get<T>() where T : class =>
            _data.TryGetValue(typeof(T), out var rawValue) && rawValue is T value
                ? value
                : Maybe.None<T>();
    }

    public class ItemData
    {
        readonly GenericData _genericData;

        internal ItemData(
            string name,
            ItemSize size,
            bool isStorable,
            int weight,
            IEnumerable<HolsterItemData> holsters,
            GenericData genericData)
        {
            Name = name;
            Size = size;
            IsStorable = isStorable;
            Weight = weight;
            Holsters = holsters.ToArray();

            _genericData = genericData;
        }

        public string Name { get; }

        public ItemSize Size { get; }

        public bool IsStorable { get; }

        public int Weight { get; }

        public IReadOnlyList<HolsterItemData> Holsters { get; }

        public Maybe<T> Get<T>() where T : class => _genericData.Get<T>();
    }

    public class HolsterItemData
    {
        readonly GenericData _genericData;

        internal HolsterItemData(string holsterName, GenericData genericData)
        {
            _genericData = genericData;
            HolsterName = holsterName;
        }

        public string HolsterName { get; }

        public Maybe<T> Get<T>() where T : class => _genericData.Get<T>();
    }
}