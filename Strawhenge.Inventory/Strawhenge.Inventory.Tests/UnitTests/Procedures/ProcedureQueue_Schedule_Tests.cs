﻿using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Tests.Fakes;
using Xunit;

namespace Strawhenge.Inventory.Tests.UnitTests.Procedures
{
    public class ProcedureQueue_Schedule_Tests
    {
        [Fact]
        public void Schedule_ShouldBeginInitialProcedure()
        {
            var subject = new ProcedureQueue();
            var procedure = new ProcedureFake();

            Assert.False(procedure.HasBegan);

            subject.Schedule(procedure);

            Assert.True(procedure.HasBegan);
        }

        [Fact]
        public void Schedule_GivenInitialProcedureEnded_ShouldBeginSecondProcedure()
        {
            var subject = new ProcedureQueue();
            var procedure1 = new ProcedureFake();
            var procedure2 = new ProcedureFake();
            subject.Schedule(procedure1);
            subject.Schedule(procedure2);

            Assert.False(procedure2.HasBegan);

            procedure1.End();

            Assert.True(procedure2.HasBegan);
        }
    }
}
