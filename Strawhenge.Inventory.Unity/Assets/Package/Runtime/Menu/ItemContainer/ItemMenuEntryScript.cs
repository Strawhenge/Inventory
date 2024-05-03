using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Unity.Data;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity
{
    public class ItemMenuEntryScript : MonoBehaviour
    {
        [SerializeField] Text _itemNameText;
        [SerializeField] Button _holdLeftHandButton;
        [SerializeField] Button _holdRightHandButton;

        IInventory _inventory;
        IContainedItem<IItemData> _containedItem;

        void Awake()
        {
            _holdLeftHandButton.onClick.AddListener(OnHoldLeftHandButton);
            _holdRightHandButton.onClick.AddListener(OnHoldRightHandButton);
        }

        public void Set(IInventory inventory, IContainedItem<IItemData> containedItem)
        {
            _inventory = inventory;
            _containedItem = containedItem;

            _itemNameText.text = containedItem.Item.Name;
        }

        void OnHoldLeftHandButton() => Hold(i => i.HoldLeftHand());

        void OnHoldRightHandButton() => Hold(i => i.HoldRightHand());

        void Hold(Action<IItem> hold)
        {
            if (_inventory == null || _containedItem == null)
                return;

            var item = _inventory.CreateItem(_containedItem.Item);
            item.ClearFromHandsPreference = ClearFromHandsPreference.Drop;
            item.ClearFromHolsterPreference = ClearFromHolsterPreference.Drop;
            hold(item);

            _containedItem.RemoveFromContainer();
        }
    }
}