using FunctionalUtilities;
using Strawhenge.Common.Unity;
using Strawhenge.Inventory.Effects;

public class EffectFactoryLocatorScript : Strawhenge.Inventory.Unity.Effects.EffectFactoryLocatorScript
{
    IncreaseArmourEffectFactory _increaseArmourEffectFactory;
    IncreaseHealthEffectFactory _increaseHealthEffectFactory;

    void Awake()
    {
        var logger = new UnityLogger(gameObject);
        _increaseArmourEffectFactory = new IncreaseArmourEffectFactory(logger);
        _increaseHealthEffectFactory = new IncreaseHealthEffectFactory(logger);
    }

    public override Maybe<IEffectFactory<TData>> Find<TData>()
    {
        if (_increaseArmourEffectFactory is IEffectFactory<TData> increaseArmourEffectFactory)
            return Maybe.Some(increaseArmourEffectFactory);

        if (_increaseHealthEffectFactory is IEffectFactory<TData> increaseHealthEffectFactory)
            return Maybe.Some(increaseHealthEffectFactory);

        return Maybe.None<IEffectFactory<TData>>();
    }
}