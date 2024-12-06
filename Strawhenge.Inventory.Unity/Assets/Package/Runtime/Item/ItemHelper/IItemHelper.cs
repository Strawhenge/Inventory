﻿using Strawhenge.Inventory.Unity.Data;
using System;

namespace Strawhenge.Inventory.Unity.Items
{
    public interface IItemHelper
    {
        event Action Released;

        IItemData Data { get; }

        void Despawn();

        ItemScript Spawn();

        void Release();
    }
}
