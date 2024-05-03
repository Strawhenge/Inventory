using Strawhenge.Inventory.Unity.Data;
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

        void OnHoldLeftHandButton()
        {
            if (_inventory == null || _containedItem == null)
                return;

            _inventory
                .CreateItem(_containedItem.Item)
                .HoldLeftHand();
            
            _containedItem.RemoveFromContainer();
        }

        void OnHoldRightHandButton()
        {
            if (_inventory == null || _containedItem == null)
                return;

            _inventory
                .CreateItem(_containedItem.Item)
                .HoldRightHand();
            
            _containedItem.RemoveFromContainer();
        }
    }
}