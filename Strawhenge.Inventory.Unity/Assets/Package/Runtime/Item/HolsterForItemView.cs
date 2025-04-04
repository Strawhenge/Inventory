using Strawhenge.Inventory.Items.Holsters;
using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Components;
using Strawhenge.Inventory.Unity.Procedures;
using System;

namespace Strawhenge.Inventory.Unity.Items
{
    public class HolsterForItemView : IHolsterForItemView
    {
        readonly IItemHelper _item;
        readonly HolsterComponent _holster;
        readonly ProcedureQueue _procedureQueue;
        readonly IProcedureFactory _procedureFactory;

        public HolsterForItemView(IItemHelper item, HolsterComponent holster, ProcedureQueue procedureQueue,
            IProcedureFactory procedureFactory)
        {
            _item = item;
            _holster = holster;
            _procedureQueue = procedureQueue;
            _procedureFactory = procedureFactory;

            item.Released += () => Released?.Invoke();
        }

        public event Action Released;

        public void DrawLeftHand(Action callback = null)
        {
            Schedule(
                _procedureFactory.DrawLeftHandFromHolster(_item, _holster), callback);
        }

        public void DrawRightHand(Action callback = null)
        {
            Schedule(
                _procedureFactory.DrawRightHandFromHolster(_item, _holster), callback);
        }

        public void Hide(Action callback = null)
        {
            Schedule(
                _procedureFactory.HideInHolster(_item, _holster), callback);
        }

        public void PutAwayLeftHand(Action callback = null)
        {
            Schedule(
                _procedureFactory.PutAwayLeftHandToHolster(_item, _holster), callback);
        }

        public void PutAwayRightHand(Action callback = null)
        {
            Schedule(
                _procedureFactory.PutAwayRightHandToHolster(_item, _holster), callback);
        }

        public void Show(Action callback = null)
        {
            Schedule(
                _procedureFactory.ShowInHolster(_item, _holster), callback);
        }

        public void Drop(Action callback = null)
        {
            Schedule(
                _procedureFactory.SpawnAndDrop(_item), callback);
        }

        void Schedule(Procedure procedure, Action callback)
        {
            _procedureQueue.Schedule(procedure);

            if (callback != null)
            {
                _procedureQueue.Schedule(() =>
                {
                    callback();
                    return Procedure.Completed;
                });
            }
        }
    }
}