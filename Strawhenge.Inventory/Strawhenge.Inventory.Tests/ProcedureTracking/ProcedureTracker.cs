using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Strawhenge.Inventory.Tests
{
    class ProcedureTracker
    {
        readonly List<ProcedureInfo> _completedProcedures = new List<ProcedureInfo>();
        readonly TestOutputLogger _logger;

        public ProcedureTracker(TestOutputLogger logger)
        {
            _logger = logger;
        }

        public void Track(TrackableProcedure procedure)
        {
            procedure.Completed += Log;
        }

        void Log(ProcedureInfo procedure)
        {
            _completedProcedures.Add(procedure);
            _logger.LogInformation(procedure.ToString());
        }

        public void VerifyProcedures(ProcedureInfo[] expectedProcedures)
        {
            var expected = expectedProcedures
                .Select(x => x.ToString())
                .ToArray();

            var actual = _completedProcedures
                .Select(x => x.ToString())
                .ToArray();

            Assert.Equal(expected, actual);
        }
    }
}