using System;
using System.Collections.Generic;
using Strawhenge.Inventory.Effects;

namespace Strawhenge.Inventory.Apparel
{
    public class ApparelPieceDataBuilder
    {
        readonly string _name;
        readonly string _slot;
        readonly IEnumerable<EffectData> _effects;
        readonly Action<IDataSetter> _setData;

        public static ApparelPieceDataBuilder Create(
            string name,
            string slot,
            IEnumerable<EffectData> effects,
            Action<IDataSetter> setData) =>
            new ApparelPieceDataBuilder(name, slot, effects, setData);

        ApparelPieceDataBuilder(string name, string slot, IEnumerable<EffectData> effects, Action<IDataSetter> setData)
        {
            _name = name;
            _slot = slot;
            _effects = effects;
            _setData = setData;
        }


        public ApparelPieceData Build()
        {
            var genericData = new GenericData();
            _setData(genericData);

            return new ApparelPieceData(_name, _slot, _effects, genericData);
        }
    }
}