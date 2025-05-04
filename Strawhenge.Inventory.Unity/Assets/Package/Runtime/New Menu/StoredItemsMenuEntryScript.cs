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

        public event Action Selected;

        public void SetItem(Item item)
        {
            _itemNameText.text = item.Name;
            _itemSelectButton.onClick.AddListener(() => Selected?.Invoke());
        }
    }
}