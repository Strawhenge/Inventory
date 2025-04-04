using Strawhenge.Inventory.Unity.Items.Data;

namespace Strawhenge.Inventory.Unity.Items
{
    public class RightHandScript : HandScript
    {
        public override IHoldItemData GetHoldItemData(IItemData itemData) => itemData.RightHandHoldData;
    }
}