using UnityEngine;

namespace Strawhenge.Inventory.Unity
{
    public class InventoryMenuScript : MonoBehaviour
    {
        [SerializeField] InventoryScript _player;
        [SerializeField] ItemMenuScript _itemMenu;

        void Start()
        {
            _player.Inventory.Hands.RightHand.Changed += () =>
            {
                if (_player.Inventory.Hands.RightHand.CurrentItem.HasSome(out var item))
                    _itemMenu.SetItem(item);
                else 
                    _itemMenu.UnsetItem();
            };
        }
    }
}