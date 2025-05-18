using Strawhenge.Common.Unity.Serialization;
using Strawhenge.Inventory.Effects;
using Strawhenge.Inventory.Unity.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Consumables
{
    [Serializable]
    public class SerializedConsumableData : IConsumableData
    {
        [SerializeField] SerializedSource<
            IConsumeAnimationSettings,
            SerializedConsumeAnimationSettings,
            ConsumeAnimationSettingsScriptableObject> _animationSettings;

        [SerializeField] EffectScriptableObject[] _effects;

        public IConsumeAnimationSettings AnimationSettings =>
            _animationSettings
                .GetValueOrDefault(() => NullConsumeAnimationSettings.Instance);

        public IEnumerable<EffectData> Effects => _effects.Select(x => x.Data);
    }
}