using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Items;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Holster
{
    public class HideInHolster : Procedure
    {
        readonly ItemHelper _itemHelper;
        readonly HolsterScript _holster;

        public HideInHolster(
            ItemHelper itemHelper,
            HolsterScript holster)
        {
            _itemHelper = itemHelper;
            _holster = holster;
        }

        protected override void OnBegin(Action endProcedure)
        {
            Hide();
            endProcedure();
        }

        protected override void OnSkip()
        {
            Hide();
        }

        void Hide()
        {
            _holster.UnsetItem();
            _itemHelper.Despawn();
        }
    }
}