using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Menu
{
    public abstract class InventoryMenuScript : MonoBehaviour
    {
        public abstract event Action Opened;

        public abstract event Action Closed;
        
        public abstract bool IsOpen { get; }

        public abstract void Open();

        public abstract void Close();
    }
}