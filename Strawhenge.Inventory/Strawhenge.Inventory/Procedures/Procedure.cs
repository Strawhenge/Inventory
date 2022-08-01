using System;

namespace Strawhenge.Inventory.Procedures
{
    public abstract class Procedure
    {
        public static Procedure Completed { get; } = new CompletedProcedure();

        internal void Begin(Action endProcedure) => OnBegin(endProcedure);

        internal void Skip() => OnSkip();

        protected abstract void OnBegin(Action endProcedure);

        protected abstract void OnSkip();

        private class CompletedProcedure : Procedure
        {
            protected override void OnBegin(Action endProcedure) => endProcedure();

            protected override void OnSkip()
            {
            }
        }
    }
}
