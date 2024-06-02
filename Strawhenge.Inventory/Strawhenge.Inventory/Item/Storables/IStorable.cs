using System;

namespace Strawhenge.Inventory.Items.Storables
{
    public interface IStorable
    {
        int Weight { get; }

        bool IsStored { get; }

        StoreItemResult AddToStorage();

        void RemoveFromStorage();
    }
}