using System.Collections.Generic;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.Drop
{
    public class Drop_from_holster : BaseItemTest
    {
        public Drop_from_holster(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            AddHolster(RightHipHolster);

            var hammer = CreateItem(Hammer, new[] { RightHipHolster });
            hammer.Holsters[RightHipHolster].Do(x => x.Equip());
            hammer.Drop();
        }

        protected override IEnumerable<ProcedureInfo> ExpectedProceduresCompleted()
        {
            yield return (Hammer, RightHipHolster, Show);
            yield return (Hammer, RightHipHolster, Drop);
        }
    }
}