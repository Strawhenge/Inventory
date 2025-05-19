using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items.HoldAnimationSettings
{
    [Serializable]
    public class SerializedHoldAnimationSettings : IHoldAnimationSettings
    {
        [SerializeField] string[] _animationFlags;

        public IReadOnlyList<string> AnimationFlags => _animationFlags
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();
    }
}