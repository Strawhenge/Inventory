using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Items;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity.NewMenu
{
    public class HolstersMenuEntryScript : MonoBehaviour
    {
        [SerializeField] Text _holsterNameText;
        [SerializeField] Text _itemNameText;
        [SerializeField] Button _itemSelectButton;

        internal event Action<Item> Selected;

        internal void SetHolster(ItemContainer holster)
        {
            _holsterNameText.text = holster.Name;
            _itemNameText.text = string.Empty;

            _itemSelectButton.interactable = false;
            _itemSelectButton.onClick.AddListener(() => holster.CurrentItem.Do(item => Selected?.Invoke(item)));

            holster.Changed += () =>
            {
                if (holster.CurrentItem.HasSome(out var item))
                {
                    _itemNameText.text = item.Name;
                    _itemSelectButton.interactable = true;
                }
                else
                {
                    _itemNameText.text = string.Empty;
                    _itemSelectButton.interactable = false;
                }
            };
        }
    }
}