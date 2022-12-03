using System;

namespace Strawhenge.Inventory.Unity.Editor
{
    public class EditorTarget<T> where T : class
    {
        readonly Func<T> _getTarget;
        T _instance;

        public EditorTarget(Func<T> getTarget)
        {
            _getTarget = getTarget;
        }

        public T Instance
        {
            get
            {
                _instance ??= _getTarget();
                return _instance;
            }
        }

        public bool HasInstance => Instance != null;
    }
}
