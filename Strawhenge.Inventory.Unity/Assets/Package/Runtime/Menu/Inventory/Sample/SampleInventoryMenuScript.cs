using Strawhenge.Common;
using Strawhenge.Common.Unity;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity.Menu.SampleInventoryMenu
{
    public class SampleInventoryMenuScript : InventoryMenuScript
    {
        [SerializeField] InventoryScript _inventory;
        [SerializeField] EventScriptableObject[] _openEvents;
        [SerializeField] UnityEvent _opened;
        [SerializeField] EventScriptableObject[] _closeEvents;
        [SerializeField] UnityEvent _closed;

        [SerializeField] RectTransform _containerPanel;
        [SerializeField] HandsMenuScript _handsMenu;
        [SerializeField] HolstersMenuScript _holstersMenu;
        [SerializeField] StoredItemsMenuScript _storedItemsMenu;
        [SerializeField] ApparelSlotsMenuScript _apparelSlotsMenu;
        [SerializeField] Button _handsMenuButton;
        [SerializeField] Button _holstersMenuButton;
        [SerializeField] Button _storageMenuButton;
        [SerializeField] Button _apparelMenuButton;

        public override event Action Opened;

        public override event Action Closed;

        void Awake()
        {
            _containerPanel.gameObject.SetActive(false);
        }

        void Start()
        {
            _handsMenu.SetInventory(_inventory.Inventory);
            _holstersMenu.SetInventory(_inventory.Inventory);
            _storedItemsMenu.SetInventory(_inventory.Inventory);
            _apparelSlotsMenu.SetInventory(_inventory.Inventory);

            _handsMenuButton.onClick.AddListener(SelectHandsMenu);
            _holstersMenuButton.onClick.AddListener(SelectHolstersMenu);
            _storageMenuButton.onClick.AddListener(SelectStoredItemsMenu);
            _apparelMenuButton.onClick.AddListener(SelectApparelMenu);

            SelectHandsMenu();
        }

        public override bool IsOpen => _containerPanel.gameObject.activeSelf;

        [ContextMenu(nameof(Open))]
        public override void Open()
        {
            _containerPanel.gameObject.SetActive(true);
            _openEvents.ForEach(x => x.Invoke(gameObject));
            _opened.Invoke();
            Opened?.Invoke();
        }

        [ContextMenu(nameof(Close))]
        public override void Close()
        {
            _containerPanel.gameObject.SetActive(false);
            _closeEvents.ForEach(x => x.Invoke(gameObject));
            _closed.Invoke();
            Closed?.Invoke();
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