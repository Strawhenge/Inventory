using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Unity.Items.Data.ScriptableObjects;
using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Loader
{
    [Serializable]
    class SerializedLoadInventoryItem : ILoadInventoryItem
    {
        [SerializeField] ItemScriptableObject _item;
        [SerializeField, Tooltip("Optional.")] HolsterScriptableObject _holster;
        [SerializeField] bool _isInStorage;
        [SerializeField] LoadInventoryItemInHand _inHand;

        public ItemData ItemData => _item.ToItemData();

        public Maybe<string> HolsterName => _holster?.Name ?? Maybe.None<string>();

        public bool IsInStorage => _isInStorage;

        public LoadInventoryItemInHand InHand => _inHand;
    }
}