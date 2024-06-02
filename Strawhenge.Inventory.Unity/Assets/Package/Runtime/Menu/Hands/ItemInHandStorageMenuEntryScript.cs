using Strawhenge.Inventory.Items.Storables;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity
{
    public class ItemInHandStorageMenuEntryScript : MonoBehaviour
    {
        [SerializeField] Button _addButton;
        [SerializeField] Button _removeButton;

        IStorable _storable;

        void Awake()
        {
            _addButton.onClick.AddListener(Add);
            _removeButton.onClick.AddListener(Remove);
        }

        internal void Set(IStorable storable)
        {
            _storable = storable;

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
    }
}