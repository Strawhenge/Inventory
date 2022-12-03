using System;

namespace Strawhenge.Inventory.Procedures
{
    class ProcedureProgress
    {
        readonly Procedure _procedure;
        readonly Action _onComplete;

        bool _hasEnded;

        public ProcedureProgress(Procedure procedure, Action onComplete)
        {
            _procedure = procedure;
            _onComplete = onComplete;
        }

        public void Begin()
        {
            _procedure.Begin(OnEnded);
        }

        public void Skip()
        {
            _hasEnded = true;
            _procedure.Skip();
        }

        void OnEnded()
        {
            if (_hasEnded) return;

            _hasEnded = true;
            _onComplete();
        }
    }
}
