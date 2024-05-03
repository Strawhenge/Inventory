using Strawhenge.Inventory.Unity.Apparel;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity
{
    public class ApparelPieceMenuEntryScript : MonoBehaviour
    {
        [SerializeField] Text _slotNameText;
        [SerializeField] Text _itemNameText;
        [SerializeField] Button _equipButton;

        IInventory _inventory;
        IContainedItem<IApparelPieceData> _containedItem;

        void Awake()
        {
            _equipButton.onClick.AddListener(OnEquipButton);
        }

        public void Set(IInventory inventory, IContainedItem<IApparelPieceData> containedItem)
        {
            _inventory = inventory;
            _containedItem = containedItem;

            _slotNameText.text = containedItem.Item.Slot;
            _itemNameText.text = containedItem.Item.Name;
        }

        void OnEquipButton()
        {
            if (_inventory == null || _containedItem == null)
                return;

            _inventory
                .CreateApparelPiece(_containedItem.Item)
                .Equip();

            _containedItem.RemoveFromContainer();
        }
    }
}