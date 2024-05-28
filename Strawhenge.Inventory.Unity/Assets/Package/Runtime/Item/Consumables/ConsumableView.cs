using Strawhenge.Inventory.Items.Consumables;
using Strawhenge.Inventory.Procedures;
using Strawhenge.Inventory.Unity.Procedures;
using System;

namespace Strawhenge.Inventory.Unity.Items.Consumables
{
    public class ConsumableView : IConsumableView
    {
        readonly ProcedureQueue _procedureQueue;
        readonly IProcedureFactory _procedureFactory;
        readonly IConsumableData _data;

        public ConsumableView(ProcedureQueue procedureQueue, IProcedureFactory procedureFactory, IConsumableData data)
        {
            _procedureQueue = procedureQueue;
            _procedureFactory = procedureFactory;
            _data = data;
        }

        public void ConsumeLeftHand(Action callback = null)
        {
            Schedule(
                _procedureFactory.ConsumeLeftHand(_data), callback);
        }

        public void ConsumeRightHand(Action callback = null)
        {
            Schedule(
                _procedureFactory.ConsumeRightHand(_data), callback);
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