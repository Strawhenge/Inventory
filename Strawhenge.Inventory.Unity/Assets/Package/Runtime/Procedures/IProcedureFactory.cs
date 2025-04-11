using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Consumables;

namespace Strawhenge.Inventory.Unity.Procedures
{
    public interface IProcedureFactory
    {
        Procedure AppearLeftHand(ItemHelper item);
        
        Procedure AppearRightHand(ItemHelper item);
        
        Procedure DrawLeftHandFromHammerspace(ItemHelper item);

        Procedure DrawRightHandFromHammerspace(ItemHelper item);

        Procedure PutAwayLeftHandToHammerspace(ItemHelper item);

        Procedure PutAwayRightHandToHammerspace(ItemHelper item);

        Procedure DropFromLeftHand(ItemHelper item);

        Procedure DropFromRightHand(ItemHelper item);

        Procedure SwapFromLeftHandToRightHand(ItemHelper item);

        Procedure SwapFromRightHandToLeftHand(ItemHelper item);

        Procedure SpawnAndDrop(ItemHelper item);

        Procedure DisappearLeftHand(ItemHelper item);
       
        Procedure DisappearRightHand(ItemHelper item);

        Procedure DrawLeftHandFromHolster(ItemHelper item, HolsterScript holster);

        Procedure DrawRightHandFromHolster(ItemHelper item, HolsterScript holster);

        Procedure PutAwayLeftHandToHolster(ItemHelper item, HolsterScript holster);

        Procedure PutAwayRightHandToHolster(ItemHelper item, HolsterScript holster);

        Procedure ShowInHolster(ItemHelper item, HolsterScript holster);

        Procedure HideInHolster(ItemHelper item, HolsterScript holster);

        Procedure ConsumeLeftHand(IConsumableData data);

        Procedure ConsumeRightHand(IConsumableData data);
    }
}