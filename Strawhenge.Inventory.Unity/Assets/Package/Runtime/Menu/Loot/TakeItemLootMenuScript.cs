using Strawhenge.Inventory.Items;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity
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

        Item _item;

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

        internal void Show(Item item)
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

        public void Hide()
        {
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

        void HoldLeftHand()
        {
            _item?.HoldLeftHand();
            Hide();
        }

        void HoldRightHand()
        {
            _item?.HoldRightHand();
            Hide();
        }

        void HolsterEquip()
        {
            _item?
                .Holsters[_holsterListDropdown.options[_holsterListDropdown.value].text]
                .Do(x => x.Equip());

            Hide();
        }

        void AddToStorage()
        {
            _item?.Storable.Do(x => x.AddToStorage());
            Hide();
        }

        void ConsumeLeftHand()
        {
            _item?.Consumable.Do(x => x.ConsumeLeftHand());
            Hide();
        }

        void ConsumeRightHand()
        {
            _item?.Consumable.Do(x => x.ConsumeRightHand());
            Hide();
        }
    }
}