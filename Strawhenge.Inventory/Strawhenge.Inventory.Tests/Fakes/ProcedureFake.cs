using Strawhenge.Inventory.Procedures;
using System;

namespace Strawhenge.Inventory.Tests.Fakes
{
    public class ProcedureFake : Procedure
    {
        Action endProcedure;

        public bool HasBegan { get; private set; } = false;

        public bool HasSkipped { get; private set; } = false;

        public void End() => endProcedure();

        protected override void OnBegin(Action endProcedure)
        {
            HasBegan = true;
            this.endProcedure = endProcedure;
        }

        protected override void OnSkip()
        {
            HasSkipped = true;
        }
    }
}
