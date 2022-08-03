using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Procedures;
using System;

namespace Strawhenge.Inventory.Unity.Items
{
    public class ItemView : IItemView
    {
        private readonly IItemHelper item;
        private readonly ProcedureQueue procedureQueue;
        private readonly IProcedureFactory procedureFactory;

        public ItemView(IItemHelper item, ProcedureQueue procedureQueue, IProcedureFactory procedureFactory)
        {
            this.item = item;
            this.procedureQueue = procedureQueue;
            this.procedureFactory = procedureFactory;

            item.Released += () => Released?.Invoke();
        }

        public event Action Released;

        public void Disappear(Action callback = null)
        {
            Schedule(
                procedureFactory.Disappear(item), callback);
        }

        public void DrawLeftHand(Action callback = null)
        {
            Schedule(
                procedureFactory.DrawLeftHandFromHammerspace(item), callback);
        }

        public void DrawRightHand(Action callback = null)
        {
            Schedule(
                procedureFactory.DrawRightHandFromHammerspace(item), callback);
        }

        public void DropLeftHand(Action callback = null)
        {
            Schedule(
                procedureFactory.DropFromLeftHand(item), callback);
        }

        public void DropRightHand(Action callback = null)
        {
            Schedule(
                procedureFactory.DropFromRightHand(item), callback);
        }

        public void LeftHandToRightHand(Action callback = null)
        {
            Schedule(
                procedureFactory.SwapFromLeftHandToRightHand(item), callback);
        }

        public void PutAwayLeftHand(Action callback = null)
        {
            Schedule(
                procedureFactory.PutAwayLeftHandToHammerspace(item), callback);
        }

        public void PutAwayRightHand(Action callback = null)
        {
            Schedule(
                procedureFactory.PutAwayRightHandToHammerspace(item), callback);
        }

        public void RightHandToLeftHand(Action callback = null)
        {
            Schedule(
                procedureFactory.SwapFromRightHandToLeftHand(item), callback);
        }

        public void SpawnAndDrop(Action callback = null)
        {
            Schedule(
                procedureFactory.SpawnAndDrop(item), callback);
        }

        private void Schedule(Procedure procedure, Action callback)
        {
            procedureQueue.Schedule(procedure);

            if (callback != null)
            {
                procedureQueue.Schedule(() =>
                {
                    callback();
                    return Procedure.Completed;
                });
            }
        }
    }
}
