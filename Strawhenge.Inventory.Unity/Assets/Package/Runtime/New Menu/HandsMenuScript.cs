using Strawhenge.Inventory.Containers;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.NewMenu
{
    public class HandsMenuScript : MonoBehaviour
    {
        [SerializeField] ItemMenuScript _leftHandItemMenu;
        [SerializeField] ItemMenuScript _rightHandItemMenu;

        public void SetInventory(Inventory inventory)
        {
            Configure(inventory.Hands.LeftHand, _leftHandItemMenu);
            Configure(inventory.Hands.RightHand, _rightHandItemMenu);
        }

        static void Configure(ItemContainer hand, ItemMenuScript itemMenu)
        {
            hand.Changed += () =>
            {
                if (hand.CurrentItem.HasSome(out var item))
                    itemMenu.SetItem(item);
                else
                    itemMenu.UnsetItem();
            };
        }
    }
}