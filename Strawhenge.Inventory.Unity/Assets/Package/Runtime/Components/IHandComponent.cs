using Strawhenge.Inventory.Unity.Items;

namespace Strawhenge.Inventory.Unity.Components
{
    public interface IHandComponent
    {
        Maybe<IItemHelper> Item { get; }

        void SetItem(IItemHelper item);

        IItemHelper TakeItem();
    }
}