using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Monobehaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strawhenge.Inventory.Unity
{
    public class ItemContainerMenuScript : MonoBehaviour
    {
        [SerializeField] InventoryScript _inventoryScript;

        public void Open(IItemContainerSource source)
        {
        }

        public void Close()
        {
        }
    }

    public interface IItemContainerSource
    {
        IReadOnlyList<IContainedItem<IApparelPieceData>> ApparelPieces { get; }
    }

    public interface IContainedItem<T>
    {
        T Item { get; }

        void RemoveFromContainer();
    }
}