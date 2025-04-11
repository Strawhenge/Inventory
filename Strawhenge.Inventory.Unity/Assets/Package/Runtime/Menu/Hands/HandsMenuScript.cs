using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity.Menu.Hands
{
    public class HandsMenuScript : MonoBehaviour
    {
        [SerializeField] ItemInHandMenuEntryScript _leftHandMenu;
        [SerializeField] ItemInHandMenuEntryScript _rightHandMenu;
        [SerializeField] Button _swapHandButton;

        IInventory _inventory;

        void Awake()
        {
            _swapHandButton.onClick.AddListener(OnSwapHandButton);
        }

        internal void Set(IInventory inventory)
        {
            _inventory = inventory;
            _leftHandMenu.Set(inventory.LeftHand);
            _rightHandMenu.Set(inventory.RightHand);
        }

        void OnSwapHandButton()
        {
            if (_inventory == null)
                return;

            _inventory.SwapHands();
        }
    }
}