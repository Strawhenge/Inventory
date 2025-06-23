using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Loot;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity.Menu.SampleLootMenu
{
    public class ItemLootMenuEntryScript : MonoBehaviour
    {
        [SerializeField] Text _itemNameText;
        [SerializeField] Button _takeButton;

        Inventory _inventory;
        Loot<Item> _containedItem;
        TakeItemLootMenuScript _takeItemLootMenu;

        void Awake()
        {
            _takeButton.onClick.AddListener(Take);
        }

        internal void Set(
            Inventory inventory,
            Loot<Item> loot,
            TakeItemLootMenuScript takeItemLootMenu)
        {
            _inventory = inventory;
            _containedItem = loot;
            _takeItemLootMenu = takeItemLootMenu;

            _itemNameText.text = loot.Content.Name;
        }

        void Take()
        {
            if (_inventory == null || _containedItem == null || _takeItemLootMenu == null)
                return;

            var item = _inventory.CreateItem(_containedItem.Take());
            _takeItemLootMenu.Show(item);
        }
    }
}