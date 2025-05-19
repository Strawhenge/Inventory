namespace Strawhenge.Inventory.Items
{
    public interface IItemProceduresFactory
    {
        ItemProcedureDto Create(Item itemData, Context context);
    }
}