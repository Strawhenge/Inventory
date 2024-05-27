using System;

namespace Strawhenge.Inventory.Items.Consumables
{
    public interface IConsumableView
    {
        void ConsumeLeftHand(Action callback = null);

        void ConsumeRightHand(Action callback = null);
    }
}