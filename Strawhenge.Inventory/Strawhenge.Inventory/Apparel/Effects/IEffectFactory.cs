namespace Strawhenge.Inventory.Apparel.Effects
{
    public interface IEffectFactory<TData> where TData : IEffectData
    {
        Effect Create(TData data);
    }

    public interface IEffectFactory
    {
        Effect Create(IEffectData data);
    }
}