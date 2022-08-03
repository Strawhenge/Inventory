using Strawhenge.Inventory.Apparel;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Apparel
{
    public class ApparelView : IApparelView
    {
        readonly IApparelGameObjectInitializer _gameObjectInitializer;
        readonly IApparelLayerAccessor _layerAccessor;
        readonly GameObject _prefab;
        readonly Transform _slot;
        readonly Vector3 _position;
        readonly Quaternion _rotation;
        readonly Vector3 _scale;

        GameObject _apparelGameObject;

        public ApparelView(
            IApparelGameObjectInitializer gameObjectInitializer,
            IApparelLayerAccessor layerAccessor,
            GameObject prefab,
            Transform slot,
            Vector3 position,
            Quaternion rotation,
            Vector3 scale)
        {
            _gameObjectInitializer = gameObjectInitializer;
            _layerAccessor = layerAccessor;
            _prefab = prefab;
            _slot = slot;
            _position = position;
            _rotation = rotation;
            _scale = scale;
        }

        public void Equip()
        {
            _apparelGameObject = Object.Instantiate(_prefab, _slot);
            _apparelGameObject.transform.localPosition = _position;
            _apparelGameObject.transform.localRotation = _rotation;
            _apparelGameObject.transform.localScale = _scale;
            _apparelGameObject.SetLayerIncludingChildren(_layerAccessor.Layer);

            foreach (var collider in _apparelGameObject.GetComponentsInChildren<Collider>())
                foreach (var slotCollider in _slot.root.gameObject.GetComponentsInChildren<Collider>())
                    Physics.IgnoreCollision(collider, slotCollider);

            _gameObjectInitializer.Initialize(_apparelGameObject);
        }

        public void Unequip()
        {
            Object.Destroy(_apparelGameObject);
        }
    }
}
