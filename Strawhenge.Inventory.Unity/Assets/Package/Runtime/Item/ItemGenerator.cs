﻿using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.TransientItems;
using Strawhenge.Inventory.Unity.Items.Data;

namespace Strawhenge.Inventory.Unity.Items
{
    public class ItemGenerator : IItemGenerator
    {
        readonly IItemFactory _itemFactory;
        readonly IItemRepository _itemRepository;

        public ItemGenerator(IItemFactory itemFactory, IItemRepository itemRepository)
        {
            _itemFactory = itemFactory;
            _itemRepository = itemRepository;
        }

        public Maybe<Item> GenerateByName(string name)
        {
            return _itemRepository
                .FindByName(name)
                .Map(_itemFactory.CreateTransient);
        }
    }
}