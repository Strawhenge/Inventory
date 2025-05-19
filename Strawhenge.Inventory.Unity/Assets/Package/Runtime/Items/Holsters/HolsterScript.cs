using Strawhenge.Common.Unity;
using Strawhenge.Inventory.Unity.Items.HolsterItemData;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Strawhenge.Inventory.Unity.Items
{
    public class HolsterScript : MonoBehaviour
    {
        [FormerlySerializedAs("holster"), SerializeField]
        HolsterScriptableObject _holster;

        [SerializeField] UnityEvent<ItemScript> _itemSet;
        [SerializeField] UnityEvent _itemUnset;

        public string HolsterName => _holster.Name;

        public PositionAndRotation GetItemDropPoint() => transform.GetPositionAndRotation();

        public void SetItem(ItemScriptInstance item, IHolsterItemData data)
        {
            var itemScript = item.Spawn();
            var itemTransform = itemScript.transform;
            itemTransform.parent = transform;
            itemTransform.localPosition = data.PositionOffset;
            itemTransform.localRotation = data.RotationOffset;

            _itemSet.Invoke(itemScript);
        }

        public void UnsetItem()
        {
            _itemUnset.Invoke();
        }
    }
}