using System;
using Strawhenge.Inventory.Procedures;

namespace Strawhenge.Inventory.Tests
{
    class TrackableProcedure : Procedure
    {
        readonly ProcedureInfo _info;

        public new event Action<ProcedureInfo> Completed;

        public TrackableProcedure(string itemName, string procedureName)
        {
            _info = new ProcedureInfo(itemName, procedureName);
        }

        public TrackableProcedure(string itemName, string holsterName, string procedureName)
        {
            _info = new ProcedureInfo(itemName, holsterName, procedureName);
        }

        protected override void OnBegin(Action endProcedure)
        {
            Completed?.Invoke(_info);
            endProcedure();
        }

        protected override void OnSkip()
        {
        }
    }
}