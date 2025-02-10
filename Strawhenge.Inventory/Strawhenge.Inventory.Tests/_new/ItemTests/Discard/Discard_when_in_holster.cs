using System.Collections.Generic;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests._new.ItemTests.Discard
{
    public class Discard_when_in_holster : BaseItemTest
    {
        public Discard_when_in_holster(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            AddHolster(RightHipHolster);
            
            var hammer = CreateItem(Hammer, new[] { RightHipHolster });
            hammer.Holsters[RightHipHolster].Do(x => x.Equip());
            hammer.Discard();
        }

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Hammer, RightHipHolster, x => x.Show);
            yield return (Hammer, RightHipHolster, x => x.Hide);
        }
    }
}