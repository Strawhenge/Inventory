using System.Collections.Generic;

namespace Strawhenge.Inventory.Unity.Items.Data
{
    public interface IHoldAnimationSettings
    {
        IReadOnlyList<string> AnimationFlags { get; }
    }
}