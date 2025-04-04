using Strawhenge.Inventory.Unity.Items.Data;

namespace Strawhenge.Inventory.Unity.Items
{
    public class LeftHandScript : HandScript
    {
        public override IHoldItemData GetHoldItemData(IItemData itemData) => itemData.LeftHandHoldData;
    }
}