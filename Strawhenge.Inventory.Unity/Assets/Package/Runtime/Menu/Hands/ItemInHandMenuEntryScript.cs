using Strawhenge.Inventory.Containers;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity
{
    public class ItemInHandMenuEntryScript : MonoBehaviour
    {
        [SerializeField] ItemInHandHolsterMenuEntryScript _holsterMenuEntryPrefab;
        [SerializeField] RectTransform _entryContainer;
        [SerializeField] Button _dropButton;
        [SerializeField] Text _itemNameText;

        PanelContainer _container;
        IItemContainer _hand;

        void Awake()
        {
            _dropButton.onClick.AddListener(OnDropButton);

            var rectTransform = GetComponent<RectTransform>();
            _container = new PanelContainer(_entryContainer, rectTransform);
        }

        public void Set(IItemContainer hand)
        {
            _hand = hand;
            _hand.Changed += OnChanged;

            OnChanged();
        }

        void OnChanged()
        {
            _container.Clear();

            if (_hand.CurrentItem.HasSome(out var item))
            {
                _itemNameText.text = item.Name;
                _itemNameText.fontStyle = FontStyle.Normal;
                _dropButton.gameObject.SetActive(true);

                foreach (var holsterForItem in item.Holsters)
                {
                    var menuEntry = Instantiate(_holsterMenuEntryPrefab);
                    _container.Add(menuEntry.GetComponent<RectTransform>());

                    menuEntry.Set(holsterForItem);
                }
            }
            else
            {
                _itemNameText.text = "Unequipped";
                _itemNameText.fontStyle = FontStyle.Italic;
                _dropButton.gameObject.SetActive(false);
            }
        }

        void OnDropButton()
        {
            _hand?.CurrentItem.Do(x => x.Drop());
        }
    }
}