using Strawhenge.Inventory.Items.Holsters;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity.Menu.Hands
{
    public class ItemInHandHolsterMenuEntryScript : MonoBehaviour
    {
        [SerializeField] Button _equipButton;

        HolsterForItem _holster;

        void Awake()
        {
            _equipButton.onClick.AddListener(OnEquipButton);
        }

        internal void Set(HolsterForItem holster)
        {
            _holster = holster;
            _equipButton.GetComponentInChildren<Text>().text = holster.HolsterName;
        }

        void OnEquipButton()
        {
            _holster?.Equip();
        }
    }
}