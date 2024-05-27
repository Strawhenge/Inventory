﻿using Strawhenge.Inventory.Items.Consumables;
using System;

namespace Strawhenge.Inventory.Unity.Items.Consumables
{
    public class ConsumableView : IConsumableView
    {
        public void ConsumeLeftHand(Action callback = null)
        {
            callback?.Invoke();
        }

        public void ConsumeRightHand(Action callback = null)
        {
            callback?.Invoke();
        }
    }
}