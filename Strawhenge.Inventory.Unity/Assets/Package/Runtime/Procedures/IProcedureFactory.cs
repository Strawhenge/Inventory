using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Components;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Items.Consumables;

namespace Strawhenge.Inventory.Unity.Procedures
{
    public interface IProcedureFactory
    {
        Procedure DrawLeftHandFromHammerspace(IItemHelper item);

        Procedure DrawRightHandFromHammerspace(IItemHelper item);

        Procedure PutAwayLeftHandToHammerspace(IItemHelper item);

        Procedure PutAwayRightHandToHammerspace(IItemHelper item);

        Procedure DropFromLeftHand(IItemHelper item);

        Procedure DropFromRightHand(IItemHelper item);

        Procedure SwapFromLeftHandToRightHand(IItemHelper item);

        Procedure SwapFromRightHandToLeftHand(IItemHelper item);

        Procedure SpawnAndDrop(IItemHelper item);

        Procedure Disappear(IItemHelper item);

        Procedure DrawLeftHandFromHolster(IItemHelper item, IHolsterComponent holster);

        Procedure DrawRightHandFromHolster(IItemHelper item, IHolsterComponent holster);

        Procedure PutAwayLeftHandToHolster(IItemHelper item, IHolsterComponent holster);

        Procedure PutAwayRightHandToHolster(IItemHelper item, IHolsterComponent holster);

        Procedure ShowInHolster(IItemHelper item, IHolsterComponent holster);

        Procedure HideInHolster(IItemHelper item, IHolsterComponent holster);

        Procedure ConsumeLeftHand(IConsumableData data);

        Procedure ConsumeRightHand(IConsumableData data);
    }
}