using FunctionalUtilities;

namespace Strawhenge.Inventory.Unity.Items.Data
{
    public interface IItemData
    {
        ItemScript Prefab { get; }

        Maybe<ItemPickupScript> PickupPrefab { get; }

        IHoldItemData LeftHandHoldData { get; }

        IHoldItemData RightHandHoldData { get; }

        IItemAnimationSettings AnimationSettings { get; }
    }
}