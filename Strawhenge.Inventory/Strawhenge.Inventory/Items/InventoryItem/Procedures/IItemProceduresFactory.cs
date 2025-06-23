namespace Strawhenge.Inventory.Items
{
    public interface IItemProceduresFactory
    {
        ItemProcedureDto Create(Item item, Context context);
    }
}