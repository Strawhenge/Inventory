﻿using System;
using System.Collections.Generic;
using System.Linq;
using Strawhenge.Common;
using Strawhenge.Inventory.Effects;

namespace Strawhenge.Inventory.Apparel
{
    public class InventoryApparelPiece
    {
        readonly ApparelSlot _slot;
        readonly IApparelView _view;
        readonly IEnumerable<Effect> _effects;

        public InventoryApparelPiece(
            ApparelPiece piece,
            ApparelSlot slot,
            IApparelView view)
            : this(piece, slot, view, Array.Empty<Effect>())
        {
        }

        public InventoryApparelPiece(
            ApparelPiece piece,
            ApparelSlot slot,
            IApparelView view,
            IEnumerable<Effect> effects)
        {
            _slot = slot;
            _view = view;
            _effects = effects.ToArray();

            Name = piece.Name;
            Piece = piece;
        }

        public string Name { get; }

        public ApparelPiece Piece { get; }

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