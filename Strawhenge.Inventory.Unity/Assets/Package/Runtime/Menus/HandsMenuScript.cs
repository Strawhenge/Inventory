using Strawhenge.Inventory.Containers;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity
{
    public class HandsMenuScript : MonoBehaviour
    {
        [SerializeField] ItemInHandMenuEntryScript _leftHandMenu;
        [SerializeField] ItemInHandMenuEntryScript _rightHandMenu;
        [SerializeField] Button _swapHandButton;

        IItemContainer _leftHand;
        IItemContainer _rightHand;

        void Awake()
        {
            _swapHandButton.onClick.AddListener(OnSwapHandButton);
        }

        internal void Set(IItemContainer leftHand, IItemContainer rightHand)
        {
            _leftHand = leftHand;
            _leftHandMenu.Set(leftHand);

            _rightHand = rightHand;
            _rightHandMenu.Set(rightHand);
        }

        void OnSwapHandButton()
        {
            if (_leftHand == null || _rightHand == null)
                return;

            var rightHandItem = _rightHand.CurrentItem;

            _leftHand.CurrentItem.Do(x => x.HoldRightHand());
            rightHandItem.Do(x => x.HoldLeftHand());
        }
    }
}