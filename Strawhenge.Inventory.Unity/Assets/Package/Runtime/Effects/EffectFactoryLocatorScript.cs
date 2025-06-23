using FunctionalUtilities;
using Strawhenge.Inventory.Effects;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Effects
{
    public abstract class EffectFactoryLocatorScript : MonoBehaviour, IEffectFactoryLocator
    {
        public abstract Maybe<IEffectFactory<TData>> Find<TData>();
    }
}