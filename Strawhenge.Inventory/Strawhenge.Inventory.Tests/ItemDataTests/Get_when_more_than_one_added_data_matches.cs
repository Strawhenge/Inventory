using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Items;
using Moq;
using Xunit;

namespace Strawhenge.Inventory.Tests.ItemDataTests
{
    public class Get_when_more_than_one_added_data_matches
    {
        readonly Mock<ILogger> _logger = new Mock<ILogger>();
        readonly WeaponStats _first = new WeaponStats();
        readonly FireSwordStats _second = new FireSwordStats();
        readonly ShieldStats _third = new ShieldStats();
        readonly Item _item;

        public Get_when_more_than_one_added_data_matches()
        {
            _item = ItemBuilder
                .Create("Sword", ItemSize.OneHanded, isStorable: true, 1, x =>
                {
                    x.Add(_first);
                    x.Add(_second);
                    x.Add(_third);
                })
                .SetLogger(_logger.Object)
                .Build();
        }

        [Fact]
        public void Should_return_first_added_match()
        {
            Assert.Same(_first, _item.Get<WeaponStats>().Reduce(() => null));
            Assert.Same(_first, _item.Get<IWeaponStats>().Reduce(() => null));
        }

        [Fact]
        public void Should_return_only_match_without_warning()
        {
            Assert.Same(_second, _item.Get<FireSwordStats>().Reduce(() => null));
            Assert.Same(_third, _item.Get<ShieldStats>().Reduce(() => null));

            _logger.Verify(x => x.LogWarning(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void Should_log_warning_once_per_type()
        {
            _item.Get<IWeaponStats>();
            _item.Get<IWeaponStats>();
            _item.Get<WeaponStats>();

            _logger.Verify(x => x.LogWarning(It.Is<string>(m => m.Contains("'IWeaponStats'"))), Times.Once);
            _logger.Verify(x => x.LogWarning(It.Is<string>(m => m.Contains("'WeaponStats'"))), Times.Once);
        }
    }
}
