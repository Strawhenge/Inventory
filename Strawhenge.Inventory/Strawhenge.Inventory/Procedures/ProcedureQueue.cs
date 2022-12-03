using System;
using System.Collections.Generic;
using System.Linq;
using FunctionalUtilities;

namespace Strawhenge.Inventory.Procedures
{
    public class ProcedureQueue
    {
        readonly Queue<Func<Procedure>> procedures = new Queue<Func<Procedure>>();

        Maybe<ProcedureProgress> currentProgress = Maybe.None<ProcedureProgress>();
        bool isPaused = false;

        public void Schedule(Procedure procedure) => Schedule(() => procedure);

        public void Schedule(Func<Procedure> createProcedure)
        {
            procedures.Enqueue(createProcedure);
            Next();
        }

        public void Pause()
        {
            isPaused = true;
        }

        public void Resume()
        {
            isPaused = false;
            Next();
        }

        public void SkipCurrentProcedure()
        {
            currentProgress.Do(p => p.Skip());
            currentProgress = Maybe.None<ProcedureProgress>();
            Next();
        }

        public void SkipAllScheduledProcedures()
        {
            currentProgress.Do(p => p.Skip());
            currentProgress = Maybe.None<ProcedureProgress>();

            foreach (var procedure in procedures)
            {
                procedure().Skip();
            }

            procedures.Clear();
        }

        void Next()
        {
            if (isPaused || !procedures.Any() || IsProcedureInProgress) return;

            var procedure = procedures.Dequeue().Invoke();
            var progress = new ProcedureProgress(procedure, OnComplete);
            currentProgress = Maybe.Some(progress);
            progress.Begin();

            void OnComplete()
            {
                currentProgress = Maybe.None<ProcedureProgress>();
                Next();
            }
        }

        bool IsProcedureInProgress => currentProgress
            .Map(_ => true)
            .Reduce(() => false);
    }
}
