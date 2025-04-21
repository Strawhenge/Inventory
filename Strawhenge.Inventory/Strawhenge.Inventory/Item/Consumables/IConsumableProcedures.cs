using Strawhenge.Inventory.Procedures;

namespace Strawhenge.Inventory.Items.Consumables
{
    public interface IConsumableProcedures
    {
        Procedure ConsumeLeftHand();

        Procedure ConsumeRightHand();
    }
}