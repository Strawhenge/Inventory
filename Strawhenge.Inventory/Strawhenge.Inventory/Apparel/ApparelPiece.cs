namespace Strawhenge.Inventory.Apparel
{
    public class ApparelPiece : IApparelPiece
    {
        readonly ApparelSlot _slot;
        readonly IApparelView _view;

        public ApparelPiece(string name, ApparelSlot slot, IApparelView view)
        {
            _slot = slot;
            _view = view;

            Name = name;
        }

        public string Name { get; }

        public string SlotName => _slot.Name;

        public bool IsEquipped { get; private set; }

        public void Equip()
        {
            if (IsEquipped)
                return;

            IsEquipped = true;
            _slot.Set(this);
            _view.Equip();
        }

        public void Unequip()
        {
            if (!IsEquipped)
                return;

            IsEquipped = false;
            _slot.Unset();
            _view.Unequip();
        }
    }
}
