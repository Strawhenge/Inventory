using Strawhenge.Inventory.Loader;
using Strawhenge.Inventory.Unity.Items.Data.ScriptableObjects;
using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Loader
{
    [Serializable]
    class SerializedLoadInventoryItem
    {
        [SerializeField] ItemScriptableObject _item;
        [SerializeField, Tooltip("Optional.")] HolsterScriptableObject _holster;
        [SerializeField] bool _isInStorage;
        [SerializeField] LoadInventoryItemInHand _inHand;

        public LoadInventoryItem Map()
        {
            var load = new LoadInventoryItem(_item.ToItem())
            {
                InHand = _inHand,
                IsInStorage = _isInStorage
            };

            if (_holster != null)
                load.SetHolster(_holster.Name);

            return load;
        }
    }
}