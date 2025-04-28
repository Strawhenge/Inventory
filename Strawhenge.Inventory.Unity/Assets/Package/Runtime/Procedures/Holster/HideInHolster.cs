using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Items;
using System;

namespace Strawhenge.Inventory.Unity.Procedures.Holster
{
    public class HideInHolster : Procedure
    {
        readonly ItemScriptInstance _itemScriptInstance;
        readonly HolsterScript _holster;

        public HideInHolster(
            ItemScriptInstance itemScriptInstance,
            HolsterScript holster)
        {
            _itemScriptInstance = itemScriptInstance;
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
            _itemScriptInstance.Despawn();
        }
    }
}