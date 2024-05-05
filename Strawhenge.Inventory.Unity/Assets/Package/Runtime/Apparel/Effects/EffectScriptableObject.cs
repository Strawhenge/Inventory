using Strawhenge.Inventory.Apparel.Effects;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Apparel
{
    public abstract class EffectScriptableObject : ScriptableObject
    {
        public abstract EffectData Data { get; }
    }
}