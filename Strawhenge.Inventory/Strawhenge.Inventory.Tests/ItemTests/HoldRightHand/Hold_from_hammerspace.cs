using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.HoldRightHand
{
    public class Hold_from_hammerspace : BaseItemTest
    {
        readonly Item _hammer;

        public Hold_from_hammerspace(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            SetStorageCapacity(100);

            _hammer = CreateItem(Hammer, storable: true);
            _hammer.Storable.Do(x => x.AddToStorage());
            _hammer.HoldRightHand();
        }

        protected override Maybe<Item> ExpectedItemInRightHand => _hammer;

        protected override IEnumerable<Item> ExpectedItemsInStorage()
        {
            yield return _hammer;
        }

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Hammer, x => x.DrawRightHand);
        }
    }
}