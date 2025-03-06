using Strawhenge.Inventory.Items.Storables;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity
{
    public class ItemInHandStorageMenuEntryScript : MonoBehaviour
    {
        [SerializeField] Button _addButton;
        [SerializeField] Button _removeButton;

        Storable _storable;

        void Awake()
        {
            _addButton.onClick.AddListener(Add);
            _removeButton.onClick.AddListener(Remove);
        }

        void OnDestroy()
        {
            _storable.Added -= OnAdded;
            _storable.Removed -= OnRemoved;
        }

        internal void Set(Storable storable)
        {
            _storable = storable;
            _storable.Added += OnAdded;
            _storable.Removed += OnRemoved;

            if (_storable.IsStored)
            {
                _addButton.gameObject.SetActive(false);
                _removeButton.gameObject.SetActive(true);
            }
            else
            {
                _addButton.gameObject.SetActive(true);
                _removeButton.gameObject.SetActive(false);
            }
        }

        void Add()
        {
            _storable.AddToStorage();
        }

        void Remove()
        {
            _storable.RemoveFromStorage();
        }

        void OnAdded()
        {
            _addButton.gameObject.SetActive(false);
            _removeButton.gameObject.SetActive(true);
        }

        void OnRemoved()
        {
            _addButton.gameObject.SetActive(true);
            _removeButton.gameObject.SetActive(false);
        }
    }
}