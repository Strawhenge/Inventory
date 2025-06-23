using FunctionalUtilities;
using Strawhenge.Inventory.Unity.Items.DrawAnimationSettings;
using Strawhenge.Inventory.Unity.Items.HoldItemData;

namespace Strawhenge.Inventory.Unity.Items.ItemData
{
    public interface IItemData
    {
        ItemScript Prefab { get; }

        Maybe<ItemPickupScript> PickupPrefab { get; }

        IHoldItemData LeftHandHoldData { get; }

        IHoldItemData RightHandHoldData { get; }

        IDrawAnimationSettings DrawAnimationSettings { get; }
    }
}