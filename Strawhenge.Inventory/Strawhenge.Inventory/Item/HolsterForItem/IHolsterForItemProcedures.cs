using Strawhenge.Inventory.Procedures;

namespace Strawhenge.Inventory.Items.Holsters
{
    public interface IHolsterForItemProcedures
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