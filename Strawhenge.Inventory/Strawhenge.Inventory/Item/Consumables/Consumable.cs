using System;
using System.Collections.Generic;
using System.Linq;
using Strawhenge.Common;
using Strawhenge.Inventory.Effects;

namespace Strawhenge.Inventory.Items.Consumables
{
    public class Consumable
    {
        readonly Item _item;
        readonly IConsumableView _view;
        readonly Effect[] _effects;

        public Consumable(Item item, IConsumableView view, IEnumerable<Effect> effects)
        {
            _item = item;
            _view = view;
            _effects = effects.ToArray();
        }

        public void ConsumeLeftHand(Action callback = null)
        {
            _item.HoldLeftHand();
            _view.ConsumeLeftHand(() =>
            {
                _item.Discard();
                _effects.ForEach(x => x.Apply());
                callback?.Invoke();
            });
        }

        public void ConsumeRightHand(Action callback = null)
        {
            _item.HoldRightHand();
            _view.ConsumeRightHand(() =>
            {
                _item.Discard();
                _effects.ForEach(x => x.Apply());
                callback?.Invoke();
            });
        }
    }
}