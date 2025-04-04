﻿using FunctionalUtilities;
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

        IItemHelper _item;

        public string Name => _holster.Name;

        public void SetItem(IItemHelper item)
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

        public IItemHelper TakeItem()
        {
            if (_item == null)
            {
                Debug.LogError("No item in holster.");
                return new NullItemHelper();
            }

            var result = _item;
            _item = null;
            return result;
        }
    }
}