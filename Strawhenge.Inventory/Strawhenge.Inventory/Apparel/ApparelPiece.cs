using System;
using System.Collections.Generic;
using System.Linq;
using Strawhenge.Common;
using Strawhenge.Inventory.Effects;

namespace Strawhenge.Inventory.Apparel
{
    public class ApparelPiece
    {
        readonly ApparelSlot _slot;
        readonly IApparelView _view;
        readonly IEnumerable<Effect> _effects;

        public ApparelPiece(
            ApparelPieceData data,
            ApparelSlot slot,
            IApparelView view)
            : this(data, slot, view, Array.Empty<Effect>())
        {
        }

        public ApparelPiece(
            ApparelPieceData data,
            ApparelSlot slot,
            IApparelView view,
            IEnumerable<Effect> effects)
        {
            _slot = slot;
            _view = view;
            _effects = effects.ToArray();

            Name = data.Name;
            Data = data;
        }

        public string Name { get; }

        public ApparelPieceData Data { get; }

        public string SlotName => _slot.Name;

        public bool IsEquipped { get; private set; }

        public void Equip()
        {
            if (IsEquipped)
                return;

            IsEquipped = true;
            _slot.Set(this);
            _view.Show();
            _effects.ForEach(x => x.Apply());
        }

        public void Unequip() => PerformUnequip(x => x.Drop());

        public void Discard() => PerformUnequip(x => x.Hide());

        void PerformUnequip(Action<IApparelView> viewStrategy)
        {
            if (!IsEquipped)
                return;

            IsEquipped = false;
            _slot.Unset();
            viewStrategy(_view);
            _effects.ForEach(x => x.Revert());
        }
    }
}