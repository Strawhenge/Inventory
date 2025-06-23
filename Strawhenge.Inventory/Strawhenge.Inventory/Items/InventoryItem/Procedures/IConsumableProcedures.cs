using Strawhenge.Inventory.Procedures;

namespace Strawhenge.Inventory.Items
{
    public interface IConsumableProcedures
    {
        Procedure ConsumeLeftHand();

        Procedure ConsumeRightHand();
    }
}