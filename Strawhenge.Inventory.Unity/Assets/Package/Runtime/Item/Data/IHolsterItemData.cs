﻿using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items.Data
{
    public interface IHolsterItemData
    {
        string HolsterName { get; }

        Vector3 PositionOffset { get; }

        Quaternion RotationOffset { get; }

        int DrawFromHolsterRightHandId { get; }

        int PutInHolsterRightHandId { get; }

        int DrawFromHolsterLeftHandId { get; }

        int PutInHolsterLeftHandId { get; }
    }
}