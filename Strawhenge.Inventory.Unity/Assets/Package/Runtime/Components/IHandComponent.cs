using FunctionalUtilities;
using Strawhenge.Inventory.Unity.Items;
using System;

namespace Strawhenge.Inventory.Unity.Components
{
    public interface IHandComponent
    {
        event Action<ItemScript> Added;
        event Action Removed;
        
        Maybe<IItemHelper> Item { get; }

        void SetItem(IItemHelper item);

        IItemHelper TakeItem();
    }
}