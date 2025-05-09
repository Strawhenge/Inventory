using System;
using Strawhenge.Inventory.Loot;

namespace Strawhenge.Inventory.Unity
{
    public interface ILootMenu
    {
        event Action Opened;

        event Action Closed;

        bool IsOpen { get; }

        void Open(ILootSource source);

        void Close();
    }
}