using System;
using Strawhenge.Inventory.Procedures;

namespace Strawhenge.Inventory.Items
{
    class ConsumableProcedureScheduler
    {
        readonly IConsumableProcedures _procedures;
        readonly ProcedureQueue _queue;

        public ConsumableProcedureScheduler(
            IConsumableProcedures procedures,
            ProcedureQueue queue)
        {
            _procedures = procedures;
            _queue = queue;
        }

        public void ConsumeLeftHand(Action callback = null) =>
            Schedule(_procedures.ConsumeLeftHand(), callback);

        public void ConsumeRightHand(Action callback = null) =>
            Schedule(_procedures.ConsumeRightHand(), callback);

        void Schedule(Procedure procedure, Action callback)
        {
            _queue.Schedule(procedure);

            if (callback != null)
            {
                _queue.Schedule(() =>
                {
                    callback();
                    return Procedure.Completed;
                });
            }
        }
    }
}