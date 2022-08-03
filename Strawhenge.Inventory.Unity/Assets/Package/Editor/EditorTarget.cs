using System;

namespace Strawhenge.Inventory.Unity.Editor
{
    public class EditorTarget<T> where T : class
    {
        private readonly Func<T> getTarget;
        private T instance;

        public EditorTarget(Func<T> getTarget)
        {
            this.getTarget = getTarget;
        }

        public T Instance
        {
            get
            {
                instance ??= getTarget();
                return instance;
            }
        }

        public bool HasInstance => Instance != null;
    }
}
