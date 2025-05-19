using Strawhenge.Inventory.Apparel;
using UnityEngine;
using UnityEngine.UI;

namespace Strawhenge.Inventory.Unity.Menu
{
    public class ApparelSlotsMenuEntryScript : MonoBehaviour
    {
        [SerializeField] Text _apparelSlotNameText;
        [SerializeField] Text _apparelPieceNameText;
        [SerializeField] Button _unequipButton;

        InventoryApparelPiece _apparelPiece;

        internal void SetApparelSlot(ApparelSlot slot)
        {
            _apparelSlotNameText.text = slot.Name;
            _apparelPieceNameText.text = string.Empty;
            _unequipButton.interactable = false;

            slot.Changed += () =>
            {
                if (slot.CurrentPiece.HasSome(out var piece))
                {
                    _apparelPiece = piece;
                    _apparelPieceNameText.text = piece.Name;
                    _unequipButton.interactable = true;
                }
                else
                {
                    _apparelPiece = null;
                    _apparelPieceNameText.text = string.Empty;
                    _unequipButton.interactable = false;
                }
            };

            _unequipButton.onClick.AddListener(Unequip);
        }

        void Unequip() => _apparelPiece?.Unequip();
    }
}