using Strawhenge.Inventory.Items;
using Xunit;

namespace Strawhenge.Inventory.Tests.ItemDataTests
{
    public class Get_when_set_data_and_added_data_match
    {
        readonly WeaponStats _setStats = new WeaponStats();
        readonly WeaponStats _addedStats = new WeaponStats();
        readonly Item _item;

        public Get_when_set_data_and_added_data_match()
        {
            _item = ItemBuilder
                .Create("Sword", ItemSize.OneHanded, isStorable: true, 1, x =>
                {
                    x.Set<IWeaponStats>(_setStats);
                    x.Add(_addedStats);
                })
                .Build();
        }

        [Fact]
        public void Should_return_set_data_for_the_type_it_was_set_as()
        {
            _item.Get<IWeaponStats>().VerifyIsSome(_setStats);
        }

        [Fact]
        public void Should_return_added_data_for_other_types()
        {
            _item.Get<WeaponStats>().VerifyIsSome(_addedStats);
        }
    }
}
