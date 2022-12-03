using System;

namespace Strawhenge.Inventory.Procedures
{
    class ProcedureProgress
    {
        readonly Procedure procedure;
        readonly Action onComplete;

        bool hasEnded = false;

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

        void OnEnded()
        {
            if (hasEnded) return;

            hasEnded = true;
            onComplete();
        }
    }
}
