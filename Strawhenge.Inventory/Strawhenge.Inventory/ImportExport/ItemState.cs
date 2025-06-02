using System;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;

namespace Strawhenge.Inventory.ImportExport
{
    public class ItemState
    {
        public ItemState(
            Item item,
            Context context,
            ItemInHandState inHand = ItemInHandState.NotInHands,
            string holster = null,
            bool isInStorage = false)
        {
            Item = item ?? throw new ArgumentNullException(nameof(item));
            Context = context ?? throw new ArgumentNullException(nameof(context));

            InHand = inHand;
            Holster = holster ?? Maybe.None<string>();
            IsInStorage = isInStorage;
        }

        public Item Item { get; }

        public Context Context { get; }

        public ItemInHandState InHand { get; }

        public Maybe<string> Holster { get; }

        public bool IsInStorage { get; }
    }
}