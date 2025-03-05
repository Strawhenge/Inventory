using Strawhenge.Inventory.Procedures;
using Xunit;

namespace Strawhenge.Inventory.Tests.ProcedureQueueTests
{
    public class PauseTests
    {
        [Fact]
        public void Schedule_GivenQueueIsPaused_ShouldNotBeginInitialProcedure()
        {
            var subject = new ProcedureQueue();
            subject.Pause();

            var procedure = new ProcedureFake();

            subject.Schedule(procedure);

            Assert.False(procedure.HasBegan);
        }

        [Fact]
        public void Resume_GivenQueueIsPaused_ShouldBeginInitialProcedure()
        {
            var subject = new ProcedureQueue();
            subject.Pause();

            var procedure = new ProcedureFake();

            subject.Schedule(procedure);

            Assert.False(procedure.HasBegan);

            subject.Resume();

            Assert.True(procedure.HasBegan);
        }

        [Fact]
        public void Resume_GivenQueueIsPaused_AndInitialProcInProgress_ShouldNotBeginNextProcedure()
        {
            var subject = new ProcedureQueue();
            var procedure1 = new ProcedureFake();
            var procedure2 = new ProcedureFake();

            subject.Schedule(procedure1);
            subject.Schedule(procedure2);
            subject.Pause();

            Assert.True(procedure1.HasBegan);
            Assert.False(procedure2.HasBegan);

            subject.Resume();

            Assert.False(procedure2.HasBegan);
        }
    }
}
