using Strawhenge.Inventory.Unity.Data.ScriptableObjects;
using System.Linq;
using FunctionalUtilities;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Data
{
    public class ItemRepository : IItemRepository
    {
        readonly IItemData[] _scriptableObjects;

        public ItemRepository(ISettings settings)
        {
            _scriptableObjects = Resources
                .LoadAll<ItemScriptableObject>(path: settings.ItemScriptableObjectsPath)
                .ToArray<IItemData>();
        }

        public Maybe<IItemData> FindByName(string name)
        {
            return _scriptableObjects.FirstOrNone(x =>
                x.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));
        }
    }
}