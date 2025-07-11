﻿using System.Collections.Generic;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.ClearFromHands
{
    public class Clear_stored_and_holstered_item_from_hands : BaseInventoryItemTest
    {
        readonly InventoryItem _hammer;

        public Clear_stored_and_holstered_item_from_hands(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            AddHolster(RightHipHolster);
            SetStorageCapacity(10);

            _hammer = CreateItem(Hammer, new[] { RightHipHolster }, storable: true);
            _hammer.HoldRightHand();
            _hammer.Storable.Do(x => x.AddToStorage());
            _hammer.Holsters[RightHipHolster].Do(x => x.Equip());
            _hammer.ClearFromHands();
        }

        protected override IEnumerable<InventoryItem> ExpectedItemsInStorage()
        {
            yield return _hammer;
        }

        protected override IEnumerable<(string holsterName, InventoryItem expectedItem)> ExpectedItemsInHolsters()
        {
            yield return (RightHipHolster, _hammer);
        }

        protected override IEnumerable<ProcedureInfo> ExpectedProceduresCompleted()
        {
            yield return (Hammer, AppearRightHand);
            yield return (Hammer, RightHipHolster, PutAwayRightHand);
        }
    }
}