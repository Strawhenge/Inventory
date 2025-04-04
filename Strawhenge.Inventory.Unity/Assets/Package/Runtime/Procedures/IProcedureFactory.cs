using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Components;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Consumables;

namespace Strawhenge.Inventory.Unity.Procedures
{
    public interface IProcedureFactory
    {
        Procedure AppearLeftHand(IItemHelper item);
        
        Procedure AppearRightHand(IItemHelper item);
        
        Procedure DrawLeftHandFromHammerspace(IItemHelper item);

        Procedure DrawRightHandFromHammerspace(IItemHelper item);

        Procedure PutAwayLeftHandToHammerspace(IItemHelper item);

        Procedure PutAwayRightHandToHammerspace(IItemHelper item);

        Procedure DropFromLeftHand(IItemHelper item);

        Procedure DropFromRightHand(IItemHelper item);

        Procedure SwapFromLeftHandToRightHand(IItemHelper item);

        Procedure SwapFromRightHandToLeftHand(IItemHelper item);

        Procedure SpawnAndDrop(IItemHelper item);

        Procedure DisappearLeftHand(IItemHelper item);
       
        Procedure DisappearRightHand(IItemHelper item);

        Procedure DrawLeftHandFromHolster(IItemHelper item, HolsterComponent holster);

        Procedure DrawRightHandFromHolster(IItemHelper item, HolsterComponent holster);

        Procedure PutAwayLeftHandToHolster(IItemHelper item, HolsterComponent holster);

        Procedure PutAwayRightHandToHolster(IItemHelper item, HolsterComponent holster);

        Procedure ShowInHolster(IItemHelper item, HolsterComponent holster);

        Procedure HideInHolster(IItemHelper item, HolsterComponent holster);

        Procedure ConsumeLeftHand(IConsumableData data);

        Procedure ConsumeRightHand(IConsumableData data);
    }
}