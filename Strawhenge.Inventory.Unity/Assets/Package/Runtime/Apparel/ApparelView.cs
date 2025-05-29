using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Unity.Apparel.ApparelPieceData;
using Strawhenge.Inventory.Unity.Loot;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Apparel
{
    public class ApparelView : IApparelView
    {
        readonly ApparelPiece _apparelPiece;
        readonly LootDrop _lootDrop;
        readonly PrefabInstantiatedEvents _prefabInstantiatedEvents;
        readonly ApparelSlotScript _slot;

        ApparelPieceScript _apparelPieceScript;

        public ApparelView(
            ApparelPiece apparelPiece,
            ApparelSlotScript slot,
            LootDrop lootDrop,
            PrefabInstantiatedEvents prefabInstantiatedEvents)
        {
            _apparelPiece = apparelPiece;
            _lootDrop = lootDrop;
            _prefabInstantiatedEvents = prefabInstantiatedEvents;
            _slot = slot;
        }

        public void Show()
        {
            _apparelPiece
                .Get<IApparelPieceData>()
                .Do(data =>
                {
                    _apparelPieceScript = Object.Instantiate(data.Prefab, _slot.transform);
                    _prefabInstantiatedEvents.Invoke(_apparelPieceScript);

                    _apparelPieceScript.transform.localPosition = data.Position;
                    _apparelPieceScript.transform.localRotation = data.Rotation;
                    _apparelPieceScript.transform.localScale = data.Scale;
                });
        }

        public void Hide()
        {
            Object.Destroy(_apparelPieceScript);
        }

        public void Drop()
        {
            Object.Destroy(_apparelPieceScript);
            _lootDrop.Drop(_apparelPiece);
        }
    }
}