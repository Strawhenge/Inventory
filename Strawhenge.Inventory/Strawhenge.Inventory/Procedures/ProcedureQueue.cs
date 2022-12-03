using System;
using System.Collections.Generic;
using System.Linq;
using FunctionalUtilities;

namespace Strawhenge.Inventory.Procedures
{
    public class ProcedureQueue
    {
        readonly Queue<Func<Procedure>> _procedures = new Queue<Func<Procedure>>();

        Maybe<ProcedureProgress> _currentProgress = Maybe.None<ProcedureProgress>();
        bool _isPaused;

        public void Schedule(Procedure procedure) => Schedule(() => procedure);

        public void Schedule(Func<Procedure> createProcedure)
        {
            _procedures.Enqueue(createProcedure);
            Next();
        }

        public void Pause()
        {
            _isPaused = true;
        }

        public void Resume()
        {
            _isPaused = false;
            Next();
        }

        public void SkipCurrentProcedure()
        {
            _currentProgress.Do(p => p.Skip());
            _currentProgress = Maybe.None<ProcedureProgress>();
            Next();
        }

        public void SkipAllScheduledProcedures()
        {
            _currentProgress.Do(p => p.Skip());
            _currentProgress = Maybe.None<ProcedureProgress>();

            foreach (var procedure in _procedures)
            {
                procedure().Skip();
            }

            _procedures.Clear();
        }

        void Next()
        {
            if (_isPaused || !_procedures.Any() || IsProcedureInProgress) return;

            var procedure = _procedures.Dequeue().Invoke();
            var progress = new ProcedureProgress(procedure, OnComplete);
            _currentProgress = Maybe.Some(progress);
            progress.Begin();

            void OnComplete()
            {
                _currentProgress = Maybe.None<ProcedureProgress>();
                Next();
            }
        }

        bool IsProcedureInProgress => _currentProgress
            .Map(_ => true)
            .Reduce(() => false);
    }
}