using Strawhenge.Inventory.Loot;
using Strawhenge.Inventory.Unity.Items;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Loot
{
    public class LootCollectionItemContextHandlerScript : ItemContextHandlerScript
    {
        [SerializeField] LootCollectionScript _lootCollection;

        public override void Handle(Context context)
        {
            context
                .Get<LootCollectionSource>()
                .Do(source => _lootCollection.Merge(source));

            context
                .Set(_lootCollection.Source);
        }
    }
}