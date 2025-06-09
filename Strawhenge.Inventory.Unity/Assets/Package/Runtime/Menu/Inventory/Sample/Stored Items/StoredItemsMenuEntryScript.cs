using Strawhenge.Inventory.Items;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity.Menu.SampleInventoryMenu
{
    public class StoredItemsMenuEntryScript : MonoBehaviour
    {
        [SerializeField] Button _itemSelectButton;
        [SerializeField] Text _itemNameText;

        internal event Action Selected;

        internal void SetItem(InventoryItem item)
        {
            _itemNameText.text = item.Name;
            _itemSelectButton.onClick.AddListener(() => Selected?.Invoke());
        }
    }
}