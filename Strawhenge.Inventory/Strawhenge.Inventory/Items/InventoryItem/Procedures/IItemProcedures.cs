using Strawhenge.Inventory.Procedures;

namespace Strawhenge.Inventory.Items
{
    public interface IItemProcedures
    {
        Procedure AppearLeftHand();

        Procedure AppearRightHand();

        Procedure DrawLeftHand();

        Procedure DrawRightHand();

        Procedure PutAwayLeftHand();

        Procedure PutAwayRightHand();

        Procedure DropLeftHand();

        Procedure DropRightHand();

        Procedure SpawnAndDrop();

        Procedure LeftHandToRightHand();

        Procedure RightHandToLeftHand();

        Procedure DisappearLeftHand();

        Procedure DisappearRightHand();
    }
}