namespace Strawhenge.Inventory.TransientItems
{
    public interface IItemGenerator
    {
        Maybe<IItem> GenerateByName(string name);
    }
}
