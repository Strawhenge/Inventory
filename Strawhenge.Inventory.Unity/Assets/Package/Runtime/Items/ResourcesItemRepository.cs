using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Unity.Items.ItemData;
using System.Collections.Generic;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items
{
    public class ResourcesItemRepository : IItemRepository
    {
        static ResourcesItemRepository _instance;

        public static ResourcesItemRepository GetOrCreateInstance()
        {
            if (_instance == null)
            {
                var items = Resources.LoadAll<ItemScriptableObject>(string.Empty);

                var itemsByName = new Dictionary<string, ItemScriptableObject>();
                foreach (var item in items)
                {
                    if (itemsByName.ContainsKey(item.name))
                        Debug.LogWarning($"Duplicate item named '{item.name}'.", item);

                    itemsByName[item.name] = item;
                }

                _instance = new ResourcesItemRepository(itemsByName);
            }

            return _instance;
        }

        readonly Dictionary<string, ItemScriptableObject> _itemsByName;

        ResourcesItemRepository(Dictionary<string, ItemScriptableObject> itemsByName)
        {
            _itemsByName = itemsByName;
        }

        public Maybe<Item> FindByName(string name)
        {
            return _itemsByName
                .MaybeGetValue(name)
                .Map(x => x.ToItem());
        }
    }
}