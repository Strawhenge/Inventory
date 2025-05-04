using Strawhenge.Inventory.Apparel;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Apparel
{
    public class ApparelView : IApparelView
    {
        readonly ApparelPieceData _data;
        readonly IApparelDrop _apparelDrop;
        readonly Transform _slot;

        GameObject _apparelGameObject;

        public ApparelView(
            ApparelPieceData data,
            IApparelDrop apparelDrop,
            Transform slot)
        {
            _data = data;
            _apparelDrop = apparelDrop;
            _slot = slot;
        }

        public void Show()
        {
            _data
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
            _apparelDrop.Drop(_data);
        }
    }
}