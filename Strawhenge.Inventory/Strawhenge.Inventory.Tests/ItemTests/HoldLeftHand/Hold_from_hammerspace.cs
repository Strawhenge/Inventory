using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.HoldLeftHand
{
    public class Hold_from_hammerspace : BaseItemTest
    {
        readonly Item _hammer;

        public Hold_from_hammerspace(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            SetStorageCapacity(100);

            _hammer = CreateItem(Hammer, storable: true);
            _hammer.Storable.Do(x => x.AddToStorage());
            _hammer.HoldLeftHand();
        }

        protected override Maybe<Item> ExpectedItemInLeftHand => _hammer;

        protected override IEnumerable<Item> ExpectedItemsInStorage()
        {
            yield return _hammer;
        }

        protected override IEnumerable<ProcedureInfo> ExpectedProceduresCompleted()
        {
            yield return (Hammer, DrawLeftHand);
        }
    }
}