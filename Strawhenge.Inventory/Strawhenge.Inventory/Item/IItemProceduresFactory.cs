namespace Strawhenge.Inventory.Items
{
    public interface IItemProceduresFactory
    {
        ItemProcedureDto Create(ItemData itemData, Context context);
    }
}