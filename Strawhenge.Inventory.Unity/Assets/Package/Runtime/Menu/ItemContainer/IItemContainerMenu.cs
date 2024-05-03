using System;

namespace Strawhenge.Inventory.Unity
{
    public interface IItemContainerMenu
    {
        event Action Opened;

        event Action Closed;

        bool IsOpen { get; }

        void Open(IItemContainerSource source);

        void Close();
    }
}