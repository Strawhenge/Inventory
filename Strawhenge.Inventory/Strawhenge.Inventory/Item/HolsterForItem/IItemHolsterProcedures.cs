using Strawhenge.Inventory.Procedures;

namespace Strawhenge.Inventory.Items.Holsters
{
    public interface IItemHolsterProcedures
    {
        Procedure DrawLeftHand();

        Procedure DrawRightHand();

        Procedure PutAwayLeftHand();

        Procedure PutAwayRightHand();

        Procedure Show();

        Procedure Hide();

        Procedure Drop();
    }
}