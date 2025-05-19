using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Unity.Items.ItemData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items
{
    public class ResourcesItemRepository : IItemRepository
    {
        readonly Dictionary<string, ItemScriptableObject> _scriptableObjects;

        public ResourcesItemRepository()
        {
            _scriptableObjects = Resources
                .LoadAll<ItemScriptableObject>(string.Empty)
                .ToDictionary(item => item.name.ToLower(), item => item);
        }

        public Maybe<Item> FindByName(string name)
        {
            return _scriptableObjects
                .MaybeGetValue(name.ToLower())
                .Map(x => x.ToItem());
        }
    }
}