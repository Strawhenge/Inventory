namespace Strawhenge.Inventory.Items
{
    public interface IDataSetter
    {
        void Set<T>(T value) where T : class;
    }
}