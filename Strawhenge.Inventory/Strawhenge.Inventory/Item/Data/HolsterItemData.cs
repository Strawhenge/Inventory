using FunctionalUtilities;

namespace Strawhenge.Inventory.Items
{
    public class HolsterItemData
    {
        readonly GenericData _genericData;

        internal HolsterItemData(string holsterName, GenericData genericData)
        {
            _genericData = genericData;
            HolsterName = holsterName;
        }

        public string HolsterName { get; }

        public Maybe<T> Get<T>() where T : class => _genericData.Get<T>();
    }
}