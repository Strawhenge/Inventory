using Strawhenge.Inventory.Effects;
using Strawhenge.Inventory.Unity.Items.ConsumeAnimationSettings;
using System.Collections.Generic;

namespace Strawhenge.Inventory.Unity.Items.ConsumableData
{
    public interface IConsumableData
    {
        IConsumeAnimationSettings AnimationSettings { get; }

        IEnumerable<EffectData> Effects { get; }
    }
}