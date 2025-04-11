using System.Collections.Generic;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.PutAway
{
    public class Put_away_when_unequipped_from_holster : BaseItemTest
    {
        public Put_away_when_unequipped_from_holster(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            AddHolster(RightHipHolster);
            
            var hammer = CreateItem(Hammer, new[] { RightHipHolster });
            hammer.Holsters[RightHipHolster].Do(x => x.Equip());
            hammer.HoldRightHand();
            hammer.UnequipFromHolster();
            hammer.PutAway();
        }

        protected override IEnumerable<ProcedureInfo> ExpectedProceduresCompleted()
        {
            yield return (Hammer, RightHipHolster, Show);
            yield return (Hammer, RightHipHolster, DrawRightHand);
            yield return (Hammer, PutAwayRightHand);
        }
    }
}