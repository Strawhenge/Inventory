using Strawhenge.Inventory.Procedures;
using Xunit;

namespace Strawhenge.Inventory.Tests.ProcedureQueueTests
{
    public class SkipTests
    {
        [Fact]
        public void Skip_GivenInitialProcInProgress_ShouldSkipInitialProc()
        {
            var subject = new ProcedureQueue();
            var procedure = new ProcedureFake();
            subject.Schedule(procedure);

            Assert.True(procedure.HasBegan);
            Assert.False(procedure.HasSkipped);

            subject.SkipCurrentProcedure();

            Assert.True(procedure.HasSkipped);
        }

        [Fact]
        public void Skip_GivenInitialProcInProgress_ShouldBeginNextProc()
        {
            var subject = new ProcedureQueue();
            var procedure1 = new ProcedureFake();
            var procedure2 = new ProcedureFake();
            subject.Schedule(procedure1);
            subject.Schedule(procedure2);

            Assert.True(procedure1.HasBegan);
            Assert.False(procedure1.HasSkipped);
            Assert.False(procedure2.HasSkipped);

            subject.SkipCurrentProcedure();

            Assert.True(procedure1.HasSkipped);
            Assert.True(procedure2.HasBegan);
        }

        [Fact]
        public void Skip_GivenInitialProcInProgress_AndQueueIsPaused_ShouldNotBeginNextProc()
        {
            var subject = new ProcedureQueue();
            var procedure1 = new ProcedureFake();
            var procedure2 = new ProcedureFake();
            subject.Schedule(procedure1);
            subject.Schedule(procedure2);

            Assert.True(procedure1.HasBegan);
            Assert.False(procedure1.HasSkipped);
            Assert.False(procedure2.HasSkipped);

            subject.Pause();
            subject.SkipCurrentProcedure();

            Assert.True(procedure1.HasSkipped);
            Assert.False(procedure2.HasBegan);
        }

        [Fact]
        public void SkipAll_GivenInitialProcInProgress_ShouldSkipInitialProc()
        {
            var subject = new ProcedureQueue();
            var procedure = new ProcedureFake();
            subject.Schedule(procedure);

            Assert.True(procedure.HasBegan);
            Assert.False(procedure.HasSkipped);

            subject.SkipAllScheduledProcedures();

            Assert.True(procedure.HasSkipped);
        }

        [Fact]
        public void SkipAll_GivenInitialProcInProgress_ShouldSkipNextProc()
        {
            var subject = new ProcedureQueue();
            var procedure1 = new ProcedureFake();
            var procedure2 = new ProcedureFake();
            subject.Schedule(procedure1);
            subject.Schedule(procedure2);

            Assert.True(procedure1.HasBegan);
            Assert.False(procedure1.HasSkipped);
            Assert.False(procedure2.HasSkipped);

            subject.SkipAllScheduledProcedures();

            Assert.True(procedure1.HasSkipped);
            Assert.False(procedure2.HasBegan);
            Assert.True(procedure2.HasSkipped);
        }

        [Fact]
        public void SkipAll_GivenPaused_ShouldSkipAllProcs()
        {
            var subject = new ProcedureQueue();
            var procedure1 = new ProcedureFake();
            var procedure2 = new ProcedureFake();
            subject.Pause();
            subject.Schedule(procedure1);
            subject.Schedule(procedure2);

            Assert.False(procedure1.HasBegan);
            Assert.False(procedure1.HasSkipped);
            Assert.False(procedure2.HasSkipped);

            subject.SkipAllScheduledProcedures();

            Assert.True(procedure1.HasSkipped);
            Assert.True(procedure2.HasSkipped);
        }
    }
}
