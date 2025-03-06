using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Procedures;
using System;

namespace Strawhenge.Inventory.Unity.Items
{
    public class ItemView : IItemView
    {
        readonly IItemHelper _item;
        readonly ProcedureQueue _procedureQueue;
        readonly IProcedureFactory _procedureFactory;

        public ItemView(IItemHelper item, ProcedureQueue procedureQueue, IProcedureFactory procedureFactory)
        {
            _item = item;
            _procedureQueue = procedureQueue;
            _procedureFactory = procedureFactory;

            item.Released += () => Released?.Invoke();
        }

        public event Action Released;

        public void DisappearLeftHand(Action callback = null)
        {
            Schedule(
                _procedureFactory.DisappearLeftHand(_item), callback);
        }

        public void DisappearRightHand(Action callback = null)
        {
            Schedule(
                _procedureFactory.DisappearRightHand(_item), callback);
        }

        public void AppearLeftHand(Action callback = null)
        {
            Schedule(
                _procedureFactory.AppearLeftHand(_item), callback);
        }

        public void AppearRightHand(Action callback = null)
        {
            Schedule(
                _procedureFactory.AppearRightHand(_item), callback);
        }

        public void DrawLeftHand(Action callback = null)
        {
            Schedule(
                _procedureFactory.DrawLeftHandFromHammerspace(_item), callback);
        }

        public void DrawRightHand(Action callback = null)
        {
            Schedule(
                _procedureFactory.DrawRightHandFromHammerspace(_item), callback);
        }

        public void DropLeftHand(Action callback = null)
        {
            Schedule(
                _procedureFactory.DropFromLeftHand(_item), callback);
        }

        public void DropRightHand(Action callback = null)
        {
            Schedule(
                _procedureFactory.DropFromRightHand(_item), callback);
        }

        public void LeftHandToRightHand(Action callback = null)
        {
            Schedule(
                _procedureFactory.SwapFromLeftHandToRightHand(_item), callback);
        }

        public void PutAwayLeftHand(Action callback = null)
        {
            Schedule(
                _procedureFactory.PutAwayLeftHandToHammerspace(_item), callback);
        }

        public void PutAwayRightHand(Action callback = null)
        {
            Schedule(
                _procedureFactory.PutAwayRightHandToHammerspace(_item), callback);
        }

        public void RightHandToLeftHand(Action callback = null)
        {
            Schedule(
                _procedureFactory.SwapFromRightHandToLeftHand(_item), callback);
        }

        public void SpawnAndDrop(Action callback = null)
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