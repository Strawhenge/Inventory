using System;
using System.Collections.Generic;

namespace Strawhenge.Inventory.Unity.Items.HoldAnimationSettings
{
    public class NullHoldAnimationSettings : IHoldAnimationSettings
    {
        public static IHoldAnimationSettings Instance { get; } = new NullHoldAnimationSettings();

        NullHoldAnimationSettings()
        {
        }

        public IReadOnlyList<string> AnimationFlags => Array.Empty<string>();
    }
}