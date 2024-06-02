﻿using System;
using System.Linq;

namespace Strawhenge.Inventory.Items.Storables
{
    public class Storable : IStorable
    {
        readonly IItem _item;
        readonly StoredItems _storedItems;

        public Storable(IItem item, StoredItems storedItems, int weight)
        {
            _item = item;
            _storedItems = storedItems;
            Weight = weight;
        }

        public int Weight { get; }

        public bool IsStored => _storedItems.Items.Contains(_item);

        public StoreItemResult AddToStorage()
        {
            // TODO weight check

            _storedItems.Add(_item);
            return StoreItemResult.Ok;
        }

        public void RemoveFromStorage()
        {
            _storedItems.Remove(_item);
        }
    }
}