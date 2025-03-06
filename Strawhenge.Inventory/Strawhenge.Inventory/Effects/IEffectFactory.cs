namespace Strawhenge.Inventory.Effects
{
    public interface IEffectFactory<TData>
    {
        Effect Create(TData data);
    }
}