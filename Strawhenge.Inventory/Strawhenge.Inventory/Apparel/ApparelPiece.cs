using System;
using System.Collections.Generic;
using System.Linq;
using Strawhenge.Common;
using Strawhenge.Inventory.Apparel.Effects;

namespace Strawhenge.Inventory.Apparel
{
    public class ApparelPiece : IApparelPiece
    {
        readonly ApparelSlot _slot;
        readonly IApparelView _view;
        readonly IEnumerable<Effect> _effects;

        public ApparelPiece(string name, ApparelSlot slot, IApparelView view)
            : this(name, slot, view, Array.Empty<Effect>())
        {
        }

        public ApparelPiece(string name, ApparelSlot slot, IApparelView view, IEnumerable<Effect> effects)
        {
            _slot = slot;
            _view = view;
            _effects = effects.ToArray();

            Name = name;
        }

        public string Name { get; }

        public string SlotName => _slot.Name;

        public bool IsEquipped { get; private set; }

        public UnequipPreference UnequipPreference { private get; set; } = UnequipPreference.Disappear;

        public void Equip()
        {
            if (IsEquipped)
                return;

            IsEquipped = true;
            _slot.Set(this);
            _view.Equip();
            _effects.ForEach(x => x.Apply());
        }

        public void Unequip()
        {
            if (!IsEquipped)
                return;

            IsEquipped = false;
            _slot.Unset();
            UnequipPreference.PerformUnequip(_view);
            _effects.ForEach(x => x.Revert());
        }
    }
}