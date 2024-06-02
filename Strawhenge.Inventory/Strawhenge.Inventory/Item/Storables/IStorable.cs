using System;

namespace Strawhenge.Inventory.Items.Storables
{
    public interface IStorable
    {
        event Action Added;
        
        event Action Removed;
        
        int Weight { get; }

        bool IsStored { get; }

        StoreItemResult AddToStorage();

        void RemoveFromStorage();
    }
}