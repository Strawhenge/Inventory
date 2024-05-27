using System;

namespace Strawhenge.Inventory.Items.Consumables
{
    public class Consumable : IConsumable
    {
        readonly IItem _item;
        readonly IConsumableView _view;

        public Consumable(IItem item, IConsumableView view)
        {
            _item = item;
            _view = view;
        }

        public void ConsumeLeftHand(Action callback = null)
        {
            _item.HoldLeftHand();
            _view.ConsumeLeftHand(() =>
            {
                _item.Discard();
                callback?.Invoke();
            });
        }

        public void ConsumeRightHand(Action callback = null)
        {
            _item.HoldRightHand();
            _view.ConsumeRightHand(() =>
            {
                _item.Discard();
                callback?.Invoke();
            });
        }
    }
}