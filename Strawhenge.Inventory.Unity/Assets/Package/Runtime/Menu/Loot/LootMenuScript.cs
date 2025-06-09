using Strawhenge.Inventory.Loot;
using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Menu
{
    public abstract class LootMenuScript : MonoBehaviour
    {
        public abstract event Action Opened;

        public abstract event Action Closed;

        public abstract bool IsOpen { get; }

        public abstract void Open(ILootSource source);

        public abstract void Close();
    }
}