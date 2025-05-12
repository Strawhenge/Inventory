using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Loot;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity.Menu
{
    public class ApparelPieceLootMenuEntryScript : MonoBehaviour
    {
        [SerializeField] Text _itemNameText;
        [SerializeField] Button _takeButton;

        Inventory _inventory;
        Loot<ApparelPieceData> _containedItem;

        void Awake()
        {
            _takeButton.onClick.AddListener(Take);
        }

        public void Set(Inventory inventory, Loot<ApparelPieceData> containedItem)
        {
            _inventory = inventory;
            _containedItem = containedItem;
           
            _itemNameText.text = containedItem.Item.Name;
        }

        void Take()
        {
            if (_inventory == null || _containedItem == null)
                return;

            var apparelPiece = _inventory.CreateApparelPiece(_containedItem.Take());
            apparelPiece.Equip();
        }
    }
}