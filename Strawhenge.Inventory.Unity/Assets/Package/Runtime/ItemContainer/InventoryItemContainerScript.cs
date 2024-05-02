using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Strawhenge.Inventory.Unity
{
    public class InventoryItemContainerScript : MonoBehaviour
    {
        public IItemContainerSource Source => throw new NotImplementedException();
    }
}