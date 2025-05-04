using Strawhenge.Inventory.Items;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity
{
    public class StoredItemsMenuEntryScript : MonoBehaviour
    {
        [SerializeField] Button _itemSelectButton;
        [SerializeField] Text _itemNameText;

        internal event Action Selected;

        internal void SetItem(Item item)
        {
            _itemNameText.text = item.Name;
            _itemSelectButton.onClick.AddListener(() => Selected?.Invoke());
        }
    }
}