using Strawhenge.Inventory.Unity.Apparel;
using Strawhenge.Inventory.Unity.Items;
using System;

namespace Strawhenge.Inventory.Unity
{
    public class PrefabInstantiatedEvents
    {
        public event Action<ItemScript> ItemInstantiated;
        public event Action<ApparelPieceScript> ApparelPieceInstantiated;

        internal void Invoke(ItemScript item) => ItemInstantiated?.Invoke(item);

        internal void Invoke(ApparelPieceScript apparelPiece) => ApparelPieceInstantiated?.Invoke(apparelPiece);
    }
}