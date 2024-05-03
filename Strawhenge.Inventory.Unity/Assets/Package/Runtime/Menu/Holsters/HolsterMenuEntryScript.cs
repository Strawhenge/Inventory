using Strawhenge.Inventory.Containers;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity.Menu.Holsters
{
    public class HolsterMenuEntryScript : MonoBehaviour
    {
        [SerializeField] Text _holsterNameText;
        [SerializeField] Text _itemNameText;
        [SerializeField] Button _removeButton;

        IItemContainer _holster;

        void Awake()
        {
            _removeButton.onClick.AddListener(OnRemoveButton);
        }

        internal void Set(IItemContainer holster)
        {
            _holster = holster;
            _holster.Changed += OnChanged;
            _holsterNameText.text = holster.Name;
            OnChanged();
        }

        void OnChanged()
        {
            if (_holster.CurrentItem.HasSome(out var item))
            {
                _itemNameText.text = item.Name;
                _itemNameText.fontStyle = FontStyle.Normal;
                _removeButton.gameObject.SetActive(true);
            }
            else
            {
                _itemNameText.text = "Unequipped";
                _itemNameText.fontStyle = FontStyle.Italic;
                _removeButton.gameObject.SetActive(false);
            }
        }

        void OnRemoveButton()
        {
            _holster?.CurrentItem.Do(x => x.UnequipFromHolster());
        }
    }
}