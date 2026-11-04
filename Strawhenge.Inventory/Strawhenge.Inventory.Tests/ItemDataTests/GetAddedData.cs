using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Items;
using Moq;
using Xunit;

namespace Strawhenge.Inventory.Tests.ItemDataTests
{
    public class GetAddedData
    {
        readonly Mock<ILogger> _logger = new Mock<ILogger>();
        readonly FireSwordStats _stats = new FireSwordStats();
        readonly Item _item;

        public GetAddedData()
        {
            _item = ItemBuilder
                .Create("Sword", ItemSize.OneHanded, isStorable: true, 1, x => x.Add(_stats))
                .SetLogger(_logger.Object)
                .Build();
        }

        [Fact]
        public void Should_return_data_by_its_own_type()
        {
            _item.Get<FireSwordStats>().VerifyIsSome(_stats);
        }

        [Fact]
        public void Should_return_data_by_base_type()
        {
            _item.Get<WeaponStats>().VerifyIsSome(_stats);
        }

        [Fact]
        public void Should_return_data_by_interface()
        {
            _item.Get<IWeaponStats>().VerifyIsSome(_stats);
        }

        [Fact]
        public void Should_return_none_for_unrelated_type()
        {
            _item.Get<ArmourStats>().VerifyIsNone();
        }

        [Fact]
        public void Should_not_log_warning()
        {
            _item.Get<IWeaponStats>();

            _logger.Verify(x => x.LogWarning(It.IsAny<string>()), Times.Never);
        }
    }
}
