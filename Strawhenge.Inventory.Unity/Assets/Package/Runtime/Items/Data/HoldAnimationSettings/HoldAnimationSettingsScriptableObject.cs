using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items.HoldAnimationSettings
{
    [CreateAssetMenu(menuName = "Strawhenge/Inventory/Hold Animation Settings")]
    public class HoldAnimationSettingsScriptableObject : ScriptableObject, IHoldAnimationSettings
    {
        [SerializeField] string[] _animationFlags;

        public IReadOnlyList<string> AnimationFlags => _animationFlags
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();
    }
}