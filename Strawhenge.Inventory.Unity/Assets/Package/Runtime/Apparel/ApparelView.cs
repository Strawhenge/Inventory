using Strawhenge.Common.Unity;
using Strawhenge.Inventory.Apparel;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Apparel
{
    public class ApparelView : IApparelView
    {
        readonly ApparelPieceData _data;
        readonly IApparelGameObjectInitializer _gameObjectInitializer;
        readonly IApparelLayerAccessor _layerAccessor;
        readonly IApparelDrop _apparelDrop;
        readonly Transform _slot;

        GameObject _apparelGameObject;

        public ApparelView(
            ApparelPieceData data,
            IApparelGameObjectInitializer gameObjectInitializer,
            IApparelLayerAccessor layerAccessor,
            IApparelDrop apparelDrop,
            Transform slot)
        {
            _data = data;
            _gameObjectInitializer = gameObjectInitializer;
            _layerAccessor = layerAccessor;
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
                    _apparelGameObject.SetLayerIncludingChildren(_layerAccessor.Layer);

                    foreach (var collider in _apparelGameObject.GetComponentsInChildren<Collider>())
                    foreach (var slotCollider in _slot.root.gameObject.GetComponentsInChildren<Collider>())
                        Physics.IgnoreCollision(collider, slotCollider);

                    _gameObjectInitializer.Initialize(_apparelGameObject);
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