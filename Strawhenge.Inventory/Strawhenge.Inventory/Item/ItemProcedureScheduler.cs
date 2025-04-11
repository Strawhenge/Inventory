using System;
using Strawhenge.Inventory.Procedures;

namespace Strawhenge.Inventory.Items
{
    class ItemProcedureScheduler
    {
        readonly IItemProcedures _procedures;
        readonly ProcedureQueue _queue;

        public ItemProcedureScheduler(IItemProcedures procedures, ProcedureQueue queue)
        {
            _procedures = procedures;
            _queue = queue;
        }

        public void AppearLeftHand(Action callback = null) => Schedule(_procedures.AppearLeftHand(), callback);

        public void AppearRightHand(Action callback = null) => Schedule(_procedures.AppearRightHand(), callback);

        public void DrawLeftHand(Action callback = null) => Schedule(_procedures.DrawLeftHand(), callback);

        public void DrawRightHand(Action callback = null) => Schedule(_procedures.DrawRightHand(), callback);

        public void PutAwayLeftHand(Action callback = null) => Schedule(_procedures.PutAwayLeftHand(), callback);

        public void PutAwayRightHand(Action callback = null) => Schedule(_procedures.PutAwayRightHand(), callback);

        public void DropLeftHand(Action callback = null) => Schedule(_procedures.DropLeftHand(), callback);

        public void DropRightHand(Action callback = null) => Schedule(_procedures.DropRightHand(), callback);

        public void SpawnAndDrop(Action callback = null) => Schedule(_procedures.SpawnAndDrop(), callback);

        public void LeftHandToRightHand(Action callback = null) => Schedule(_procedures.LeftHandToRightHand(), callback);

        public void RightHandToLeftHand(Action callback = null) => Schedule(_procedures.RightHandToLeftHand(), callback);

        public void DisappearLeftHand(Action callback = null) => Schedule(_procedures.DisappearLeftHand(), callback);

        public void DisappearRightHand(Action callback = null) => Schedule(_procedures.DisappearRightHand(), callback);

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