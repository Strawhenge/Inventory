using Strawhenge.Common.Unity.Serialization;
using Strawhenge.Inventory.Effects;
using Strawhenge.Inventory.Unity.Effects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Consumables
{
    [CreateAssetMenu(menuName = "Strawhenge/Inventory/Consumable")]
    public class ConsumableDataScriptableObject : ScriptableObject, IConsumableData
    {
        [SerializeField] int _animationId;

        [SerializeField] SerializedSource<
            IConsumeAnimationSettings,
            SerializedConsumeAnimationSettings,
            ConsumeAnimationSettingsScriptableObject> _animationSettings;

        [SerializeField] EffectScriptableObject[] _effects;

        public int AnimationId => _animationId;

        public IConsumeAnimationSettings AnimationSettings =>
            _animationSettings
                .GetValueOrDefault(() => NullConsumeAnimationSettings.Instance);

        public IEnumerable<EffectData> Effects => _effects.Select(x => x.Data);
    }
}