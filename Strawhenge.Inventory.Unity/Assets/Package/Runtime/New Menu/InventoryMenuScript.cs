using UnityEngine;

namespace Strawhenge.Inventory.Unity.NewMenu
{
    public class InventoryMenuScript : MonoBehaviour
    {
        [SerializeField] InventoryScript _player;
        [SerializeField] HandsMenuScript _handsMenu;
        [SerializeField] HolstersMenuScript _holstersMenu;

        void Start()
        {
            _handsMenu.SetInventory(_player.Inventory);
            _holstersMenu.SetInventory(_player.Inventory);
        }
    }
}