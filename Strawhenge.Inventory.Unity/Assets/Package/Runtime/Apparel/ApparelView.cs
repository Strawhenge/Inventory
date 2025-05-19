using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Unity.Loot;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Apparel
{
    public class ApparelView : IApparelView
    {
        readonly ApparelPiece _apparelPiece;
        readonly LootDrop _lootDrop;
        readonly Transform _slot;

        GameObject _apparelGameObject;

        public ApparelView(
            ApparelPiece apparelPiece,
            LootDrop lootDrop,
            Transform slot)
        {
            _apparelPiece = apparelPiece;
            _lootDrop = lootDrop;
            _slot = slot;
        }

        public void Show()
        {
            _apparelPiece
                .Get<IApparelPieceData>()
                .Do(data =>
                {
                    _apparelGameObject = Object.Instantiate(data.Prefab, _slot);
                    _apparelGameObject.transform.localPosition = data.Position;
                    _apparelGameObject.transform.localRotation = data.Rotation;
                    _apparelGameObject.transform.localScale = data.Scale;
                });
        }

        public void Hide()
        {
            Object.Destroy(_apparelGameObject);
        }

        public void Drop()
        {
            Object.Destroy(_apparelGameObject);
            _lootDrop.Drop(_apparelPiece);
        }
    }
}