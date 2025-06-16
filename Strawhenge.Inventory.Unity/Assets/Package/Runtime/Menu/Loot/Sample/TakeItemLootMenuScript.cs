using Strawhenge.Inventory.Items;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity.Menu.SampleLootMenu
{
    public class TakeItemLootMenuScript : MonoBehaviour
    {
        [SerializeField] RectTransform _containerPanel;
        [SerializeField] Text _itemNameText;
        [SerializeField] Button _holdLeftHandButton;
        [SerializeField] Button _holdRightHandButton;
        [SerializeField] Dropdown _holsterListDropdown;
        [SerializeField] Button _holsterEquipButton;
        [SerializeField] Button _addToStorageButton;
        [SerializeField] Button _consumeLeftHandButton;
        [SerializeField] Button _consumeRightHandButton;

        InventoryItem _item;

        void Awake()
        {
            _holdLeftHandButton.onClick.AddListener(HoldLeftHand);
            _holdRightHandButton.onClick.AddListener(HoldRightHand);
            _holsterEquipButton.onClick.AddListener(HolsterEquip);
            _addToStorageButton.onClick.AddListener(AddToStorage);
            _consumeLeftHandButton.onClick.AddListener(ConsumeLeftHand);
            _consumeRightHandButton.onClick.AddListener(ConsumeRightHand);

            Hide();
        }

        internal void Show(InventoryItem item)
        {
            if (_item != null)
                _item.Drop();

            _item = item;
            _itemNameText.text = item.Name;

            _holdLeftHandButton.interactable = true;
            _holdRightHandButton.interactable = true;

            _holsterListDropdown.options.Clear();

            if (item.Holsters.Any())
            {
                _holsterListDropdown.interactable = true;
                _holsterEquipButton.interactable = true;

                _holsterListDropdown.options.AddRange(
                    _item.Holsters.Select(x => new Dropdown.OptionData(x.HolsterName)));

                _holsterListDropdown.RefreshShownValue();
            }
            else
            {
                _holsterListDropdown.interactable = false;
                _holsterEquipButton.interactable = false;
            }

            var isStorable = _item.Storable.HasSome();
            _addToStorageButton.interactable = isStorable;

            var isConsumable = _item.Consumable.HasSome();
            _consumeLeftHandButton.interactable = isConsumable;
            _consumeRightHandButton.interactable = isConsumable;

            _containerPanel.gameObject.SetActive(true);
        }

        internal void Hide()
        {
            if (_item != null)
                _item.Drop();

            _item = null;
            _itemNameText.text = string.Empty;
            _holdLeftHandButton.interactable = false;
            _holdRightHandButton.interactable = false;
            _holsterListDropdown.interactable = false;
            _holsterEquipButton.interactable = false;
            _addToStorageButton.interactable = false;
            _consumeLeftHandButton.interactable = false;
            _consumeRightHandButton.interactable = false;

            _containerPanel.gameObject.SetActive(false);
        }

        void HoldLeftHand() => Perform(i => i.HoldLeftHand());

        void HoldRightHand() => Perform(i => i.HoldRightHand());

        void HolsterEquip() => Perform(i => i
            .Holsters[_holsterListDropdown.options[_holsterListDropdown.value].text]
            .Do(x => x.Equip()));

        void AddToStorage() => Perform(i => i.Storable.Do(x => x.AddToStorage()));

        void ConsumeLeftHand() => Perform(i => i.Consumable.Do(x => x.ConsumeLeftHand()));

        void ConsumeRightHand() => Perform(i => i.Consumable.Do(x => x.ConsumeRightHand()));

        void Perform(Action<InventoryItem> action)
        {
            if (_item == null) return;

            action(_item);
            _item = null;
            Hide();
        }
    }
}