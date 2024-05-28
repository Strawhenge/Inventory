using Strawhenge.Inventory.Effects;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Effects
{
    public abstract class EffectScriptableObject : ScriptableObject
    {
        public abstract EffectData Data { get; }
    }
}