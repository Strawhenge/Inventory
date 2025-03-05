using FunctionalUtilities;
using Strawhenge.Inventory.Items;

namespace Strawhenge.Inventory.TransientItems
{
    public interface IItemGenerator
    {
        Maybe<Item> GenerateByName(string name);
    }
}
