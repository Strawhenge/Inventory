using System;
using Strawhenge.Inventory.Procedures;

namespace Strawhenge.Inventory.Items.Holsters
{
    class ItemHolsterProcedureScheduler
    {
        readonly IItemHolsterProcedures _procedures;
        readonly ProcedureQueue _queue;

        public ItemHolsterProcedureScheduler(IItemHolsterProcedures procedures, ProcedureQueue queue)
        {
            _procedures = procedures;
            _queue = queue;
        }

        public void DrawLeftHand(Action callback = null) => Schedule(_procedures.DrawLeftHand(), callback);

        public void DrawRightHand(Action callback = null) => Schedule(_procedures.DrawRightHand(), callback);

        public void PutAwayLeftHand(Action callback = null) => Schedule(_procedures.PutAwayLeftHand(), callback);

        public void PutAwayRightHand(Action callback = null) => Schedule(_procedures.PutAwayRightHand(), callback);

        public void Show(Action callback = null) => Schedule(_procedures.Show(), callback);

        public void Hide(Action callback = null) => Schedule(_procedures.Hide(), callback);

        public void Drop(Action callback = null) => Schedule(_procedures.Drop(), callback);

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