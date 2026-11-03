using System;
using UnityEngine;
using ILogger = Strawhenge.Common.Logging.ILogger;
using Object = UnityEngine.Object;

namespace Strawhenge.Inventory.Unity
{
    /// <summary>
    /// Logs to the Unity Console with any Unity object (such as a scriptable object) as the context, so clicking
    /// the message highlights it.
    /// </summary>
    class UnityObjectLogger : ILogger
    {
        readonly Object _context;

        public UnityObjectLogger(Object context)
        {
            _context = context;
        }

        public void LogInformation(string message) => Debug.Log(message, _context);

        public void LogWarning(string message) => Debug.LogWarning(message, _context);

        public void LogError(string message) => Debug.LogError(message, _context);

        public void LogException(Exception exception) => Debug.LogException(exception, _context);
    }
}
