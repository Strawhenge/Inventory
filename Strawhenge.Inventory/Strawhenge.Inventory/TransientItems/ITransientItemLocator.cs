using FunctionalUtilities;
using Strawhenge.Inventory.Items;

namespace Strawhenge.Inventory.TransientItems
{
    public interface ITransientItemLocator
    {
        Maybe<Item> GetItemByName(string name);
    }
}