﻿using System.Collections.Generic;
using FunctionalUtilities;

namespace Strawhenge.Inventory.Containers
{
    public interface IHolsters
    {
        IEnumerable<IHolster> GetAll();

        Maybe<IHolster> FindByName(string name);

        void Add(string name);
    }
}
