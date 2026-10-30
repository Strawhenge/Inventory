using FunctionalUtilities;
using Strawhenge.Inventory.Items;

namespace Strawhenge.Inventory.Unity.Items.ItemData
{
    public static class CustomItemDataExtensions
    {
        /// <summary>
        /// The first scriptable object in the item's Custom Data list that is a <typeparamref name="T"/>,
        /// including subclasses and implementations of interfaces.
        /// </summary>
        public static Maybe<T> GetCustomData<T>(this Item item) where T : class =>
            item
                .Get<CustomItemData>()
                .Map(data => data.Get<T>())
                .Flatten();

        /// <inheritdoc cref="GetCustomData{T}(Item)"/>
        public static Maybe<T> GetCustomData<T>(this InventoryItem inventoryItem) where T : class =>
            inventoryItem.Item.GetCustomData<T>();
    }
}
