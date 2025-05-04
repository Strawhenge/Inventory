using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity.NewMenu
{
    public class InventoryMenuScript : MonoBehaviour
    {
        [SerializeField] InventoryScript _player;
        [SerializeField] HandsMenuScript _handsMenu;
        [SerializeField] HolstersMenuScript _holstersMenu;
        [SerializeField] StoredItemsMenuScript _storedItemsMenu;
        [SerializeField] ApparelSlotsMenuScript _apparelSlotsMenu;
        [SerializeField] Button _handsMenuButton;
        [SerializeField] Button _holstersMenuButton;
        [SerializeField] Button _storageMenuButton;
        [SerializeField] Button _apparelMenuButton;

        void Start()
        {
            this.InvokeAsSoonAs(
                () => _player.IsConfigurationComplete,
                () =>
                {
                    _handsMenu.SetInventory(_player.Inventory);
                    _holstersMenu.SetInventory(_player.Inventory);
                    _storedItemsMenu.SetInventory(_player.Inventory);
                    _apparelSlotsMenu.SetInventory(_player.Inventory);
                });

            _handsMenuButton.onClick.AddListener(SelectHandsMenu);
            _holstersMenuButton.onClick.AddListener(SelectHolstersMenu);
            _storageMenuButton.onClick.AddListener(SelectStoredItemsMenu);
            _apparelMenuButton.onClick.AddListener(SelectApparelMenu);

            SelectHandsMenu();
        }

        void SelectHandsMenu()
        {
            _handsMenu.gameObject.SetActive(true);
            _holstersMenu.gameObject.SetActive(false);
            _storedItemsMenu.gameObject.SetActive(false);
            _apparelSlotsMenu.gameObject.SetActive(false);

            _handsMenuButton.interactable = false;
            _holstersMenuButton.interactable = true;
            _storageMenuButton.interactable = true;
            _apparelMenuButton.interactable = true;
        }

        void SelectHolstersMenu()
        {
            _handsMenu.gameObject.SetActive(false);
            _holstersMenu.gameObject.SetActive(true);
            _storedItemsMenu.gameObject.SetActive(false);
            _apparelSlotsMenu.gameObject.SetActive(false);

            _handsMenuButton.interactable = true;
            _holstersMenuButton.interactable = false;
            _storageMenuButton.interactable = true;
            _apparelMenuButton.interactable = true;
        }

        void SelectStoredItemsMenu()
        {
            _handsMenu.gameObject.SetActive(false);
            _holstersMenu.gameObject.SetActive(false);
            _storedItemsMenu.gameObject.SetActive(true);
            _apparelSlotsMenu.gameObject.SetActive(false);

            _handsMenuButton.interactable = true;
            _holstersMenuButton.interactable = true;
            _storageMenuButton.interactable = false;
            _apparelMenuButton.interactable = true;
        }

        void SelectApparelMenu()
        {
            _handsMenu.gameObject.SetActive(false);
            _holstersMenu.gameObject.SetActive(false);
            _storedItemsMenu.gameObject.SetActive(false);
            _apparelSlotsMenu.gameObject.SetActive(true);

            _handsMenuButton.interactable = true;
            _holstersMenuButton.interactable = true;
            _storageMenuButton.interactable = true;
            _apparelMenuButton.interactable = false;
        }
    }
}