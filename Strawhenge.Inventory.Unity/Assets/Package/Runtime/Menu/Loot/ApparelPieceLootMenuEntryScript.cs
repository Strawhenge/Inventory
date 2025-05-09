using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Loot;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity.Menu
{
    public class ApparelPieceLootMenuEntryScript : MonoBehaviour
    {
        [SerializeField] Text _slotNameText;
        [SerializeField] Text _itemNameText;
        [SerializeField] Button _equipButton;

        Inventory _inventory;
        Loot<ApparelPieceData> _containedItem;

        void Awake()
        {
            _equipButton.onClick.AddListener(OnEquipButton);
        }

        public void Set(Inventory inventory, Loot<ApparelPieceData> containedItem)
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

            var apparelPiece = _inventory.CreateApparelPiece(_containedItem.Item);
            apparelPiece.Equip();

            _containedItem.Take();
        }
    }
}