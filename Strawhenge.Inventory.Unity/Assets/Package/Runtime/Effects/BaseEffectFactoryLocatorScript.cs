using FunctionalUtilities;
using Strawhenge.Inventory.Effects;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Effects
{
    public abstract class BaseEffectFactoryLocatorScript : MonoBehaviour, IEffectFactoryLocator
    {
        public abstract Maybe<IEffectFactory<TData>> Find<TData>();
    }
}