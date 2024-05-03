using Strawhenge.Inventory.Items.HolsterForItem;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity.Menu.Hands
{
    public class ItemInHandHolsterMenuEntryScript : MonoBehaviour
    {
        [SerializeField] Button _equipButton;

        IEquipItemToHolster _holster;

        void Awake()
        {
            _equipButton.onClick.AddListener(OnEquipButton);
        }

        internal void Set(IEquipItemToHolster holster)
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