using System.Collections.Generic;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.Drop
{
    public class Drop_from_hammerspace : BaseItemTest
    {
        public Drop_from_hammerspace(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            var hammer = CreateItem(Hammer);
            hammer.Drop();
        }

        protected override IEnumerable<ProcedureInfo> ExpectedProceduresCompleted()
        {
            yield return (Hammer, SpawnAndDrop);
        }
    }
}