using Strawhenge.Inventory.Apparel;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity.Menu.Apparel
{
    public class ApparelSlotMenuEntryScript : MonoBehaviour
    {
        [SerializeField] Text _slotNameText;
        [SerializeField] Text _itemNameText;
        [SerializeField] Button _unequipButton;

        IApparelSlot _slot;

        void Awake()
        {
            _unequipButton.onClick.AddListener(OnUnequipButton);
        }

        internal void Set(IApparelSlot slot)
        {
            _slot = slot;
            _slot.Changed += OnChanged;
            _slotNameText.text = slot.Name;
            OnChanged();
        }

        void OnChanged()
        {
            if (_slot.CurrentPiece.HasSome(out var piece))
            {
                _unequipButton.gameObject.SetActive(true);
                _itemNameText.text = piece.Name;
                _itemNameText.fontStyle = FontStyle.Normal;
            }
            else
            {
                _unequipButton.gameObject.SetActive(false);
                _itemNameText.text = "Unequipped";
                _itemNameText.fontStyle = FontStyle.Italic;
            }
        }

        void OnUnequipButton()
        {
            _slot?.CurrentPiece.Do(x => x.Unequip());
        }
    }
}