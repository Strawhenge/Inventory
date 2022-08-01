using System;

namespace Strawhenge.Inventory.Procedures
{
    internal class ProcedureProgress
    {
        private readonly Procedure procedure;
        private readonly Action onComplete;

        private bool hasEnded = false;

        public ProcedureProgress(Procedure procedure, Action onComplete)
        {
            this.procedure = procedure;
            this.onComplete = onComplete;
        }

        public void Begin()
        {
            procedure.Begin(OnEnded);
        }

        public void Skip()
        {
            hasEnded = true;
            procedure.Skip();
        }

        private void OnEnded()
        {
            if (hasEnded) return;

            hasEnded = true;
            onComplete();
        }
    }
}
