using Strawhenge.Inventory.Effects;
using System.Collections.Generic;

namespace Strawhenge.Inventory.Unity.Consumables
{
    public interface IConsumableData
    {
        int AnimationId { get; }

        IConsumeAnimationSettings AnimationSettings { get; }

        IEnumerable<EffectData> Effects { get; }
    }
}