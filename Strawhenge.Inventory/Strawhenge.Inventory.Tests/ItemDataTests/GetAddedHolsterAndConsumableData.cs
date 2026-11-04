using System.Linq;
using FunctionalUtilities;
using Strawhenge.Inventory.Effects;
using Strawhenge.Inventory.Items;
using Xunit;

namespace Strawhenge.Inventory.Tests.ItemDataTests
{
    public class GetAddedHolsterAndConsumableData
    {
        readonly WeaponStats _holsterStats = new WeaponStats();
        readonly WeaponStats _consumableStats = new WeaponStats();
        readonly Item _item;

        public GetAddedHolsterAndConsumableData()
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
            _item.Holsters[0].Get<IWeaponStats>().VerifyIsSome(_holsterStats);
        }

        [Fact]
        public void Should_return_consumable_data_by_interface()
        {
            _item.Consumable
                .Map(consumable => consumable.Get<IWeaponStats>())
                .Flatten()
                .VerifyIsSome(_consumableStats);
        }
    }
}
