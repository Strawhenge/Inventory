using Strawhenge.Inventory.Items.HolsterForItem;
using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Components;
using Strawhenge.Inventory.Unity.Procedures;
using System;

namespace Strawhenge.Inventory.Unity.Items
{
    public class HolsterForItemView : IHolsterForItemView
    {
        readonly IItemHelper item;
        readonly IHolsterComponent holster;
        readonly ProcedureQueue procedureQueue;
        readonly IProcedureFactory procedureFactory;

        public HolsterForItemView(IItemHelper item, IHolsterComponent holster, ProcedureQueue procedureQueue, IProcedureFactory procedureFactory)
        {
            this.item = item;
            this.holster = holster;
            this.procedureQueue = procedureQueue;
            this.procedureFactory = procedureFactory;

            item.Released += () => Released?.Invoke();
        }

        public event Action Released;

        public void DrawLeftHand(Action callback = null)
        {
            Schedule(
                procedureFactory.DrawLeftHandFromHolster(item, holster), callback);
        }

        public void DrawRightHand(Action callback = null)
        {
            Schedule(
                procedureFactory.DrawRightHandFromHolster(item, holster), callback);
        }

        public void Hide(Action callback = null)
        {
            Schedule(
                procedureFactory.HideInHolster(item, holster), callback);
        }

        public void PutAwayLeftHand(Action callback = null)
        {
            Schedule(
                procedureFactory.PutAwayLeftHandToHolster(item, holster), callback);
        }

        public void PutAwayRightHand(Action callback = null)
        {
            Schedule(
                procedureFactory.PutAwayRightHandToHolster(item, holster), callback);
        }

        public void Show(Action callback = null)
        {
            Schedule(
                procedureFactory.ShowInHolster(item, holster), callback);
        }

        void Schedule(Procedure procedure, Action callback)
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
