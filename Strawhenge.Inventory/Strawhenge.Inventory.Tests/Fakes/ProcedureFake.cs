using Strawhenge.Inventory.Procedures;
using System;

namespace Strawhenge.Inventory.Tests.Fakes
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
