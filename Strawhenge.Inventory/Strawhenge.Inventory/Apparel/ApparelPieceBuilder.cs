using System;
using System.Collections.Generic;
using Strawhenge.Inventory.Effects;

namespace Strawhenge.Inventory.Apparel
{
    public class ApparelPieceBuilder
    {
        readonly string _name;
        readonly string _slot;
        readonly IEnumerable<EffectData> _effects;
        readonly Action<IDataSetter> _setData;

        public static ApparelPieceBuilder Create(
            string name,
            string slot,
            IEnumerable<EffectData> effects,
            Action<IDataSetter> setData) =>
            new ApparelPieceBuilder(name, slot, effects, setData);

        ApparelPieceBuilder(string name, string slot, IEnumerable<EffectData> effects, Action<IDataSetter> setData)
        {
            _name = name;
            _slot = slot;
            _effects = effects;
            _setData = setData;
        }


        public ApparelPiece Build()
        {
            var genericData = new GenericData();
            _setData(genericData);

            return new ApparelPiece(_name, _slot, _effects, genericData);
        }
    }
}