using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity
{
    public class ItemInHandMenuEntryScript : MonoBehaviour
    {
        [SerializeField] ItemInHandHolsterMenuEntryScript _holsterMenuEntryPrefab;
        [SerializeField] RectTransform _entryContainer;
        [SerializeField] RectTransform _dropButton;

        PanelContainer _container;

        void Awake()
        {
            var rectTransform = GetComponent<RectTransform>();
            _container = new PanelContainer(_entryContainer, rectTransform);
        }

        void Start()
        {
            _container.Add(_dropButton.GetComponent<RectTransform>());
        }
        
        [ContextMenu("Add")]
        public void Add()
        {
            var menuEntry = Instantiate(_holsterMenuEntryPrefab);

            _container.Add(menuEntry.GetComponent<RectTransform>());
        }
    }
}