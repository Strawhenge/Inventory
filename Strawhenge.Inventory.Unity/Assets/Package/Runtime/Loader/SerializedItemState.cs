using Strawhenge.Inventory.ImportExport;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.ItemData;
using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Loader
{
    [Serializable]
    class SerializedItemState
    {
        [SerializeField] ItemScriptableObject _item;
        [SerializeField] ItemInHandState _inHand;
        [SerializeField, Tooltip("Optional.")] HolsterScriptableObject _holster;
        [SerializeField] bool _isInStorage;

        public ItemState Map()
        {
            var item = _item.ToItem();

            return new ItemState(
                item,
                inHand: _inHand,
                holster: _holster?.Name,
                isInStorage: _isInStorage);
        }
    }
}