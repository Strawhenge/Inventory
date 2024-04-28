using Strawhenge.Inventory.Items.HolsterForItem;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity
{
    public class ItemInHandHolsterMenuEntryScript : MonoBehaviour
    {
        [SerializeField] Button _equipButton;

        IHolsterForItem _holsterForItem;

        void Awake()
        {
            _equipButton.onClick.AddListener(OnEquipButton);
        }

        internal void Set(IHolsterForItem holsterForItem)
        {
            _holsterForItem = holsterForItem;
            _equipButton.GetComponentInChildren<Text>().text = holsterForItem.HolsterName;
        }

        void OnEquipButton()
        {
            _holsterForItem?.Equip();
        }
    }
}