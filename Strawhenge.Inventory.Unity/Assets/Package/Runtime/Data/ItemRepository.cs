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
            _scriptableObjects = Resources.LoadAll<ItemScriptableObject>(
                path: settings.ItemScriptableObjectsPath);
        }

        public Maybe<IItemData> FindByName(string name)
        {
            var first = _scriptableObjects.FirstOrDefault(x => x.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));

            if (first == null)
                return Maybe.None<IItemData>();

            return Maybe.Some(first);
        }
    }
}
