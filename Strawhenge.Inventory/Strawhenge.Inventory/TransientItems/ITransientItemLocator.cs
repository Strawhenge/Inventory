using FunctionalUtilities;

namespace Strawhenge.Inventory.TransientItems
{
    public interface ITransientItemLocator
    {
        Maybe<IItem> GetItemByName(string name);
    }
}