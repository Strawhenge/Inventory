using System;
using System.Collections.Generic;
using FunctionalUtilities;

namespace Strawhenge.Inventory.Items
{
    public class ItemData
    {
        public string Name { get; }

        public ItemSize Size { get; }

        public bool IsStorable { get; }

        public int Weight { get; }

        public IReadOnlyList<HolsterItemData> Holsters { get; }

        public Maybe<T> Get<T>() where T : class => throw new NotImplementedException();
    }

    public class HolsterItemData
    {
        public string HolsterName { get; }

        public Maybe<T> Get<T>() where T : class => throw new NotImplementedException();
    }
}