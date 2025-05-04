using Strawhenge.Inventory.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity
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
        }

        public void SetItem(Item item)
        {
            _itemNameText.text = item.Name;
            _item = item;
        }

        public void UnsetItem()
        {
            _itemNameText.text = string.Empty;
            _item = null;
        }

        void HoldLeftHand() => _item?.HoldLeftHand();

        void HoldRightHand() => _item?.HoldRightHand();

        void SwapHands() => _item?.SwapHands();

        void ClearFromHands() => _item?.ClearFromHands();
        void PutAway() => _item?.PutAway();

        void Drop() => _item?.Drop();

        void Discard() => _item?.Discard();
    }
}