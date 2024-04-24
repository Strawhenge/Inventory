﻿using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Unity.Apparel;
using System.Collections.Generic;
using System.Linq;

namespace Strawhenge.Inventory.Unity.Loader
{
    public class LoadInventoryDataGenerator
    {
        readonly IHolsters _holsters;
        readonly ApparelManager _apparelManager;

        public LoadInventoryDataGenerator(IHolsters holsters, ApparelManager apparelManager)
        {
            _holsters = holsters;
            _apparelManager = apparelManager;
        }

        public LoadInventoryData GenerateCurrentLoadData()
        {
            return new LoadInventoryData
            {
                HolsteredItems = GetHolsteredItems().ToArray(),
                EquippedApparel = GetEquippedApparelNames().ToArray()
            };
        }

        IEnumerable<HolsteredItemLoadInventoryData> GetHolsteredItems()
        {
            foreach (var holster in _holsters.GetAll())
            {
                if (holster.CurrentItem.HasSome(out var item))
                    yield return new HolsteredItemLoadInventoryData(item.Name, holster.Name);
            }
        }

        IEnumerable<string> GetEquippedApparelNames()
        {
            foreach (var slot in _apparelManager.Slots)
            {
                if (slot.CurrentPiece.HasSome(out var apparelPiece))
                    yield return apparelPiece.Name;
            }
        }
    }
}