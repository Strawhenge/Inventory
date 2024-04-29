using System;

namespace Strawhenge.Inventory.Unity
{
    public interface IInventoryMenu
    {
        event Action Opened;
        
        event Action Closed;

        bool IsOpen { get; }

        void Open();

        void Close();
    }
}