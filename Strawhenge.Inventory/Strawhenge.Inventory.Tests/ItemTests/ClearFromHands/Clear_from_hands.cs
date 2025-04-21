using System.Collections.Generic;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.ClearFromHands
{
    public class Clear_from_hands : BaseItemTest
    {
        public Clear_from_hands(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            var hammer = CreateItem(Hammer);
            hammer.HoldRightHand();
            hammer.ClearFromHands();
        }

        protected override IEnumerable<ProcedureInfo> ExpectedProceduresCompleted()
        {
            yield return (Hammer, AppearRightHand);
            yield return (Hammer, DropRightHand);
        }
    }
}