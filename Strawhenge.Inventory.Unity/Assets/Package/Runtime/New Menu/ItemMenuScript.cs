using Strawhenge.Inventory.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity
{
    public class ItemMenuScript : MonoBehaviour
    {
        [SerializeField] Text _itemNameText;

        public void SetItem(Item item)
        {
            _itemNameText.text = item.Name;
        }

        public void UnsetItem()
        {
        }
    }
}