using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Loot;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity.Menu.SampleLootMenu
{
    public class ApparelPieceLootMenuEntryScript : MonoBehaviour
    {
        [SerializeField] Text _itemNameText;
        [SerializeField] Button _takeButton;

        Inventory _inventory;
        Loot<ApparelPiece> _containedItem;

        void Awake()
        {
            _takeButton.onClick.AddListener(Take);
        }

        internal void Set(Inventory inventory, Loot<ApparelPiece> loot)
        {
            _inventory = inventory;
            _containedItem = loot;

            _itemNameText.text = loot.Content.Name;
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