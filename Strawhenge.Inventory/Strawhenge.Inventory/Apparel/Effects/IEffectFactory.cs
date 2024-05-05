namespace Strawhenge.Inventory.Apparel.Effects
{
    public interface IEffectFactory
    {
        Effect Create(EffectData data);
    }

    public interface IEffectFactory<TData>
    {
        Effect Create(TData data);
    }
}