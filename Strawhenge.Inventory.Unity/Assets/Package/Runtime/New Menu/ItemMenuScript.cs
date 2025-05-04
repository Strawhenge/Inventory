using Strawhenge.Inventory.Items;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity.NewMenu
{
    public class ItemMenuScript : MonoBehaviour
    {
        [SerializeField] Text _itemNameText;
        [SerializeField] Button _holdLeftHandButton;
        [SerializeField] Button _holdRightHandButton;
        [SerializeField] Button _swapHandsButton;
        [SerializeField] Button _clearFromHandsButton;
        [SerializeField] Button _putAwayButton;
        [SerializeField] Button _dropButton;
        [SerializeField] Button _discardButton;
        [SerializeField] Button _unequipFromHolsterButton;
        [SerializeField] Dropdown _holsterListDropdown;
        [SerializeField] Button _holsterEquipButton;
        [SerializeField] Button _holsterUnequipButton;
        [SerializeField] Button _addToStorageButton;
        [SerializeField] Button _removeFromStorageButton;
        [SerializeField] Button _consumeLeftHandButton;
        [SerializeField] Button _consumeRightHandButton;

        Item _item;

        void Awake()
        {
            _holdLeftHandButton.onClick.AddListener(HoldLeftHand);
            _holdRightHandButton.onClick.AddListener(HoldRightHand);
            _swapHandsButton.onClick.AddListener(SwapHands);
            _clearFromHandsButton.onClick.AddListener(ClearFromHands);
            _putAwayButton.onClick.AddListener(PutAway);
            _dropButton.onClick.AddListener(Drop);
            _discardButton.onClick.AddListener(Discard);
            _unequipFromHolsterButton.onClick.AddListener(UnequipFromHolster);
            _holsterEquipButton.onClick.AddListener(HolsterEquip);
            _holsterUnequipButton.onClick.AddListener(HolsterUnequip);
            _addToStorageButton.onClick.AddListener(AddToStorage);
            _removeFromStorageButton.onClick.AddListener(RemoveFromStorage);
            _consumeLeftHandButton.onClick.AddListener(ConsumeLeftHand);
            _consumeRightHandButton.onClick.AddListener(ConsumeRightHand);

            UnsetItem();
        }

        internal void SetItem(Item item)
        {
            _item = item;
            _itemNameText.text = item.Name;

            _holdLeftHandButton.interactable = true;
            _holdRightHandButton.interactable = true;
            _swapHandsButton.interactable = true;
            _clearFromHandsButton.interactable = true;
            _putAwayButton.interactable = true;
            _dropButton.interactable = true;
            _discardButton.interactable = true;
            _unequipFromHolsterButton.interactable = true;

            _holsterListDropdown.options.Clear();

            if (item.Holsters.Any())
            {
                _holsterListDropdown.interactable = true;
                _holsterEquipButton.interactable = true;
                _holsterUnequipButton.interactable = true;

                _holsterListDropdown.options.AddRange(
                    _item.Holsters.Select(x => new Dropdown.OptionData(x.HolsterName)));

                _holsterListDropdown.RefreshShownValue();
            }
            else
            {
                _holsterListDropdown.interactable = false;
                _holsterEquipButton.interactable = false;
                _holsterUnequipButton.interactable = false;
            }

            var isStorable = _item.Storable.HasSome();
            _addToStorageButton.interactable = isStorable;
            _removeFromStorageButton.interactable = isStorable;

            var isConsumable = _item.Consumable.HasSome();
            _consumeLeftHandButton.interactable = isConsumable;
            _consumeRightHandButton.interactable = isConsumable;
        }

        public void UnsetItem()
        {
            _item = null;
            _itemNameText.text = string.Empty;
            _holdLeftHandButton.interactable = false;
            _holdRightHandButton.interactable = false;
            _swapHandsButton.interactable = false;
            _clearFromHandsButton.interactable = false;
            _putAwayButton.interactable = false;
            _dropButton.interactable = false;
            _discardButton.interactable = false;
            _unequipFromHolsterButton.interactable = false;
            _holsterListDropdown.interactable = false;
            _holsterEquipButton.interactable = false;
            _holsterUnequipButton.interactable = false;
            _addToStorageButton.interactable = false;
            _removeFromStorageButton.interactable = false;
            _consumeLeftHandButton.interactable = false;
            _consumeRightHandButton.interactable = false;
        }

        void HoldLeftHand() => _item?.HoldLeftHand();

        void HoldRightHand() => _item?.HoldRightHand();

        void SwapHands() => _item?.SwapHands();

        void ClearFromHands() => _item?.ClearFromHands();

        void PutAway() => _item?.PutAway();

        void Drop() => _item?.Drop();

        void Discard() => _item?.Discard();

        void UnequipFromHolster() => _item?.UnequipFromHolster();

        void HolsterEquip() => _item?
            .Holsters[_holsterListDropdown.options[_holsterListDropdown.value].text]
            .Do(x => x.Equip());

        void HolsterUnequip() => _item?
            .Holsters[_holsterListDropdown.options[_holsterListDropdown.value].text]
            .Do(x => x.Unequip());

        void AddToStorage() => _item?.Storable.Do(x => x.AddToStorage());

        void RemoveFromStorage() => _item?.Storable.Do(x => x.RemoveFromStorage());

        void ConsumeLeftHand() => _item?.Consumable.Do(x => x.ConsumeLeftHand());

        void ConsumeRightHand() => _item?.Consumable.Do(x => x.ConsumeRightHand());
    }
}