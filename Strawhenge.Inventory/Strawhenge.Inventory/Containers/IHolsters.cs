using System.Collections.Generic;
using FunctionalUtilities;

namespace Strawhenge.Inventory.Containers
{
    public interface IHolsters
    {
        IEnumerable<ItemContainer> GetAll();

        Maybe<ItemContainer> FindByName(string name);

        void Add(string name);
    }
}