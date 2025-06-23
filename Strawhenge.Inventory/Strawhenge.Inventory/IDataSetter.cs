namespace Strawhenge.Inventory
{
    public interface IDataSetter
    {
        void Set<T>(T value) where T : class;
    }
}