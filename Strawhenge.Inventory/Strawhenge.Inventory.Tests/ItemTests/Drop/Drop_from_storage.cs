using System.Collections.Generic;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.Drop
{
    public class Drop_from_storage : BaseItemTest
    {
        public Drop_from_storage(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            SetStorageCapacity(10);
            
            var hammer = CreateItem(Hammer, storable: true);
            hammer.Storable.Do(x => x.AddToStorage());
            hammer.Drop();
        }

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Hammer, x => x.SpawnAndDrop);
        }
    }
}