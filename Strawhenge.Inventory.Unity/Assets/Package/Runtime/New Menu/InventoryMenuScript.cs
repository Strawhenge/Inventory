using UnityEngine;

namespace Strawhenge.Inventory.Unity.NewMenu
{
    public class InventoryMenuScript : MonoBehaviour
    {
        [SerializeField] InventoryScript _player;
        [SerializeField] HandsMenuScript _handsMenu;

        void Start()
        {
            _handsMenu.SetInventory(_player.Inventory);
        }
    }
}