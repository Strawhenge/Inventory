using System;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;

namespace Strawhenge.Inventory.Containers
{
    public interface IItemContainer
    {
        event Action Changed;
        
        string Name { get; }

        Maybe<Item> CurrentItem { get; }

        bool IsCurrentItem(Item item);
    }
}