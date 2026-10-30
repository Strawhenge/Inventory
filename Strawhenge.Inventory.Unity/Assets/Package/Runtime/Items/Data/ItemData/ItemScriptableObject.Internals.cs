namespace Strawhenge.Inventory.Unity.Items.ItemData
{
    public partial class ItemScriptableObject
    {
        internal static string PrefabFieldName => nameof(_prefab);

        internal static string PickupPrefabFieldName => nameof(_pickupPrefab);

        internal static string SizeFieldName => nameof(_size);

        internal static string IsStorableFieldName => nameof(_isStorable);

        internal static string WeightFieldName => nameof(_weight);

        internal static string LeftHandHoldItemDataFieldName => nameof(_leftHandHoldItemData);

        internal static string RightHandHoldItemDataFieldName => nameof(_rightHandHoldItemData);

        internal static string HolsterItemDataFieldName => nameof(_holsterItemData);

        internal static string CustomDataFieldName => nameof(_customData);
    }
}
