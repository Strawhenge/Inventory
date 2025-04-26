using Strawhenge.Inventory.Unity.Items.Data;
using System.Linq;
using Strawhenge.Common.Logging;

namespace Strawhenge.Inventory.Unity.Items
{
    public static class ItemHelperExtensions
    {
        public static IHolsterItemData GetHolsterData(this ItemHelper item, HolsterScript holster, ILogger logger)
        {
            if (IsHolsterCompatible(item, holster, logger, out var data))
            {
                return data;
            }

            logger.LogError($"Item '{item.Data.Name}' not setup for holster '{holster.HolsterName}'.");
            return new NullHolsterItemData(holster.HolsterName);
        }

        public static bool IsHolsterCompatible(this ItemHelper item, HolsterScript holster, ILogger logger) =>
            IsHolsterCompatible(item, holster, logger, out _);

        public static bool IsHolsterCompatible(
            this ItemHelper item,
            HolsterScript holster,
            ILogger logger,
            out IHolsterItemData data)
        {
            var matchingData = item.Data.HolsterItemData
                .Where(x => holster.HolsterName.Equals(x.HolsterName))
                .ToArray();

            if (matchingData.Length > 1)
                logger.LogError($"Item '{item.Data.Name}' has multiple setups for holster '{holster.HolsterName}'.");

            data = matchingData.FirstOrDefault();
            return data != null;
        }
    }
}