using System.Collections.Generic;
using System.Linq;
using FunctionalUtilities;
using Strawhenge.Inventory.Effects;

namespace Strawhenge.Inventory.Apparel
{
    public class ApparelPiece
    {
        readonly GenericData _genericData;

        internal ApparelPiece(string name, string slot, IEnumerable<EffectData> effects, GenericData genericData)
        {
            _genericData = genericData;
            Name = name;
            Slot = slot;
            Effects = effects.ToArray();
        }

        public string Name { get; }

        public string Slot { get; }

        public IReadOnlyList<EffectData> Effects { get; }

        public Maybe<T> Get<T>() where T : class => _genericData.Get<T>();
    }
}