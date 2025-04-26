using Strawhenge.Inventory.Items;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity
{
    public class ContainedItemMenuEntryScript : MonoBehaviour
    {
        [SerializeField] Text _itemNameText;
        [SerializeField] Button _holdLeftHandButton;
        [SerializeField] Button _holdRightHandButton;

        Inventory _inventory;
        ContainedItem<ItemData> _containedItem;

        void Awake()
        {
            _holdLeftHandButton.onClick.AddListener(OnHoldLeftHandButton);
            _holdRightHandButton.onClick.AddListener(OnHoldRightHandButton);
        }

        public void Set(Inventory inventory, ContainedItem<ItemData> containedItem)
        {
            _inventory = inventory;
            _containedItem = containedItem;

            _itemNameText.text = containedItem.Item.Name;
        }

        void OnHoldLeftHandButton() => Hold(i => i.HoldLeftHand());

        void OnHoldRightHandButton() => Hold(i => i.HoldRightHand());

        void Hold(Action<Item> hold)
        {
            if (_inventory == null || _containedItem == null)
                return;

            var item = _inventory.CreateItem(_containedItem.Item);
            hold(item);

            _containedItem.RemoveFromContainer();
        }
    }
}