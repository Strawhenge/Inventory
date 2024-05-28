using System;

namespace Strawhenge.Inventory.Items.Consumables
{
    public interface IConsumable
    {
        void ConsumeLeftHand(Action callback = null);

        void ConsumeRightHand(Action callback = null);
    }
}