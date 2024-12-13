using Strawhenge.Inventory.Effects;
using System.Collections.Generic;

namespace Strawhenge.Inventory.Unity.Consumables
{
    public interface IConsumableData
    {
        int AnimationId { get; }
        
        IEnumerable<EffectData> Effects { get; }
    }
}