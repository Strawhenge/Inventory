using System.Linq;
using FunctionalUtilities;
using Strawhenge.Inventory.Effects;
using Strawhenge.Inventory.Items;
using Xunit;

namespace Strawhenge.Inventory.Tests.ItemDataTests
{
    public class Get_added_holster_and_consumable_data
    {
        readonly WeaponStats _holsterStats = new WeaponStats();
        readonly WeaponStats _consumableStats = new WeaponStats();
        readonly Item _item;

        public Get_added_holster_and_consumable_data()
        {
            _item = ItemBuilder
                .Create("Potion", ItemSize.OneHanded, isStorable: true, 1)
                .AddHolster("Belt", x => x.Add(_holsterStats))
                .SetConsumable(Enumerable.Empty<EffectData>(), x => x.Add(_consumableStats))
                .Build();
        }

        [Fact]
        public void Should_return_holster_data_by_interface()
        {
            Assert.Same(_holsterStats, _item.Holsters[0].Get<IWeaponStats>().Reduce(() => null));
        }

        [Fact]
        public void Should_return_consumable_data_by_interface()
        {
            var stats = _item.Consumable
                .Map(consumable => consumable.Get<IWeaponStats>())
                .Flatten()
                .Reduce(() => null);

            Assert.Same(_consumableStats, stats);
        }
    }
}
