using System;
using FunctionalUtilities;

namespace Strawhenge.Inventory.Containers
{
    public interface IItemContainer
    {
        event Action Changed;
        
        string Name { get; }

        Maybe<IItem> CurrentItem { get; }

        bool IsCurrentItem(IItem item);
    }
}