using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Menu.Storage
{
    public class StorageMenuScript : MonoBehaviour
    {
        [SerializeField] StoredItemMenuEntry _menuEntryPrefab;
        [SerializeField] RectTransform _entryContainer;

        [ContextMenu("Add Some")]
        public void AddSome()
        {
            for (int i = 0; i < 10; i++)
                Instantiate(_menuEntryPrefab, parent: _entryContainer);
        }
    }
}