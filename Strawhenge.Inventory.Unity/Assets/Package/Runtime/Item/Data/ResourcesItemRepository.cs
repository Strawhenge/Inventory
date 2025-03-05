using System.Linq;
using FunctionalUtilities;
using Strawhenge.Inventory.Unity.Items.Data.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items.Data
{
    public class ResourcesItemRepository : IItemRepository
    {
        readonly Dictionary<string, ItemScriptableObject> _scriptableObjects;

        public ResourcesItemRepository(ISettings settings)
        {
            _scriptableObjects = Resources
                .LoadAll<ItemScriptableObject>(path: settings.ItemScriptableObjectsPath)
                .ToDictionary(item => item.name, item => item);
        }

        public Maybe<IItemData> FindByName(string name)
        {
            if (_scriptableObjects.TryGetValue(name, out var item))
                return Maybe.Some<IItemData>(item);

            return Maybe.None<IItemData>();
        }
    }
}