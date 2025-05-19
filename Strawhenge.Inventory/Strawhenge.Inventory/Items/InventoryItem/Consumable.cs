using System;
using System.Collections.Generic;
using System.Linq;
using Strawhenge.Common;
using Strawhenge.Inventory.Effects;
using Strawhenge.Inventory.Procedures;

namespace Strawhenge.Inventory.Items
{
    public class Consumable
    {
        readonly InventoryItem _item;
        readonly Effect[] _effects;
        readonly ConsumableProcedureScheduler _procedures;

        public Consumable(
            InventoryItem item,
            IEnumerable<Effect> effects,
            IConsumableProcedures procedures,
            ProcedureQueue procedureQueue)
        {
            _item = item;
            _effects = effects.ToArray();
            _procedures = new ConsumableProcedureScheduler(procedures, procedureQueue);
        }

        public void ConsumeLeftHand(Action callback = null)
        {
            _item.HoldLeftHand();
            _procedures.ConsumeLeftHand(() =>
            {
                _item.Discard();
                _effects.ForEach(x => x.Apply());
                callback?.Invoke();
            });
        }

        public void ConsumeRightHand(Action callback = null)
        {
            _item.HoldRightHand();
            _procedures.ConsumeRightHand(() =>
            {
                _item.Discard();
                _effects.ForEach(x => x.Apply());
                callback?.Invoke();
            });
        }
    }
}