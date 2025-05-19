using System.Collections.Generic;
using System.Linq;
using FunctionalUtilities;
using Strawhenge.Inventory.Effects;

namespace Strawhenge.Inventory.Items
{
    public class ItemConsumable
    {
        readonly GenericData _genericData;

        internal ItemConsumable(IEnumerable<EffectData> effects, GenericData genericData)
        {
            _genericData = genericData;
            Effects = effects.ToArray();
        }

        public IReadOnlyList<EffectData> Effects { get; }

        public Maybe<T> Get<T>() where T : class => _genericData.Get<T>();
    }
}