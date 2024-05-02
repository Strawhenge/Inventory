using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Data;
using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity
{
    public class InventoryItemContainerScript : MonoBehaviour
    {
        public IInventory Inventory { private get; set; }

        public IItemRepository ItemRepository { private get; set; }

        public IApparelRepository ApparelRepository { private get; set; }

        public IItemContainerSource Source { get; private set; }

        void Start()
        {
            Source = new InventoryItemContainerSource(Inventory, ItemRepository, ApparelRepository);
        }
    }
}