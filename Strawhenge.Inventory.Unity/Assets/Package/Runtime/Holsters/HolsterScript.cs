using FunctionalUtilities;
using Strawhenge.Common.Unity;
using Strawhenge.Inventory.Unity.Items.Data;
using Strawhenge.Inventory.Unity.Items.Data.ScriptableObjects;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Strawhenge.Inventory.Unity.Items
{
    public class HolsterScript : MonoBehaviour
    {
        [FormerlySerializedAs("holster"), SerializeField]
        HolsterScriptableObject _holster;

        ItemHelper _item;

        public string HolsterName => _holster.Name;

        public PositionAndRotation GetItemDropPoint() => transform.GetPositionAndRotation();
        
        public void SetItem(ItemHelper item)
        {
            _item = item;

            var data = item.Data.HolsterItemData
                .Where(x => _holster.Name.Equals(x.HolsterName))
                .FirstOrNone()
                .Reduce(() =>
                {
                    Debug.LogError($"Item '{item.Data.Name}' not setup for holster '{_holster.Name}'.");
                    return new NullHolsterItemData(_holster.Name);
                });

            var itemScript = item.Spawn();
            var itemTransform = itemScript.transform;
            itemTransform.parent = transform;
            itemTransform.localPosition = data.PositionOffset;
            itemTransform.localRotation = data.RotationOffset;
        }

        public Maybe<ItemHelper> TakeItem()
        {
            if (_item == null)
            {
                Debug.LogError("No item in holster.");
                return Maybe.None<ItemHelper>();
            }

            var item = _item;
            _item = null;
            
            return Maybe.Some(item);
        }
    }
}