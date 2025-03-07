using Strawhenge.Inventory.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity.Menu.Storage
{
    public class StoredItemMenuEntryScript : MonoBehaviour
    {
        [SerializeField] Text _itemNameText;
        [SerializeField] Button _holdLeftHandButton;
        [SerializeField] Button _holdRightHandButton;
        [SerializeField] Button _consumeLeftHandButton;
        [SerializeField] Button _consumeRightHandButton;
        [SerializeField] Button _dropButton;

        Item _item;

        void Awake()
        {
            _holdLeftHandButton.onClick.AddListener(HoldLeftHand);
            _holdRightHandButton.onClick.AddListener(HoldRightHand);
            _consumeLeftHandButton.onClick.AddListener(ConsumeLeftHand);
            _consumeRightHandButton.onClick.AddListener(ConsumeRightHand);
            _dropButton.onClick.AddListener(Drop);
        }

        internal void Set(Item item)
        {
            _itemNameText.text = item.Name;
            _item = item;
        }

        void HoldLeftHand() => _item.HoldLeftHand();

        void HoldRightHand() => _item.HoldRightHand();

        void ConsumeLeftHand() => _item.Consumable.Do(x => x.ConsumeLeftHand());

        void ConsumeRightHand() => _item.Consumable.Do(x => x.ConsumeRightHand());

        void Drop() => _item.Drop();
    }
}