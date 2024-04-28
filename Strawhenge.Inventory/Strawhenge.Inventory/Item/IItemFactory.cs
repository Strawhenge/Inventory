namespace Strawhenge.Inventory
{
    public interface IItemFactory<TItemSource>
    {
        IItem Create(TItemSource source);
    }
}