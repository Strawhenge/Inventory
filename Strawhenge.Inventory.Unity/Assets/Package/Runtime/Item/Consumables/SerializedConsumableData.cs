using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items.Consumables
{
    [Serializable]
    public class SerializedConsumableData : IConsumableData
    {
        [SerializeField] int _animationId;

        public int AnimationId => _animationId;
    }
}