using System.Collections.Generic;

namespace Strawhenge.Inventory.Unity.Items.HoldAnimationSettings
{
    public interface IHoldAnimationSettings
    {
        IReadOnlyList<string> AnimationFlags { get; }
    }
}