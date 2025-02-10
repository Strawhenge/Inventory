using System;
using Strawhenge.Inventory.Procedures;

namespace Strawhenge.Inventory.Tests._new.ProcedureQueueTests
{
    public class ProcedureFake : Procedure
    {
        Action _endProcedure;

        public bool HasBegan { get; private set; }

        public bool HasSkipped { get; private set; }

        public void End() => _endProcedure();

        protected override void OnBegin(Action endProcedure)
        {
            HasBegan = true;
            _endProcedure = endProcedure;
        }

        protected override void OnSkip()
        {
            HasSkipped = true;
        }
    }
}
