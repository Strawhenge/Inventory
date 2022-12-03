using Moq;
using Strawhenge.Inventory.TransientItems;
using Xunit;

namespace Strawhenge.Inventory.Tests.UnitTests
{
    public class TransientItemHolder_Tests
    {
        readonly Mock<ITransientItemLocator> _itemLocatorMock;
        readonly Mock<IItem> _itemMock;
        readonly ITransientItemHolder _transientItemHolder;
        readonly AssertableCallback _assertableCallback;

        public TransientItemHolder_Tests()
        {
            _itemMock = new Mock<IItem>();

            _itemLocatorMock = new Mock<ITransientItemLocator>();
            _itemLocatorMock.SetupNone(x => x.GetItemByName(It.IsAny<string>()));

            _transientItemHolder = new TransientItemHolder(_itemLocatorMock.Object);

            _assertableCallback = new AssertableCallback();
        }

        [Fact]
        public void HoldLeftHand_WhenItemNotLocated()
        {
            _transientItemHolder.HoldLeftHand(It.IsAny<string>(), _assertableCallback);

            _assertableCallback.AssertCalledOnce();
        }

        [Fact]
        public void HoldLeftHand_WhenItemLocated()
        {
            ArrangeItemLocatorReturnsItem();

            _transientItemHolder.HoldLeftHand(It.IsAny<string>(), _assertableCallback);

            _itemMock.VerifyOnce(x => x.HoldLeftHand(_assertableCallback));
            _assertableCallback.AssertNeverCalled();
        }

        [Fact]
        public void HoldRightHand_WhenItemNotLocated()
        {
            _transientItemHolder.HoldRightHand(It.IsAny<string>(), _assertableCallback);

            _assertableCallback.AssertCalledOnce();
        }

        [Fact]
        public void HoldRightHand_WhenItemLocated()
        {
            ArrangeItemLocatorReturnsItem();

            _transientItemHolder.HoldRightHand(It.IsAny<string>(), _assertableCallback);

            _itemMock.VerifyOnce(x => x.HoldRightHand(_assertableCallback));
            _assertableCallback.AssertNeverCalled();
        }

        [Fact]
        public void Unhold()
        {
            _transientItemHolder.Unhold(_assertableCallback);

            _assertableCallback.AssertCalledOnce();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Unhold_WhenItemIsHeld(bool leftHand)
        {
            ArrangeItemLocatorReturnsItem();

            if (leftHand)
                _transientItemHolder.HoldLeftHand(It.IsAny<string>());
            else
                _transientItemHolder.HoldRightHand(It.IsAny<string>());

            _transientItemHolder.Unhold(_assertableCallback);

            _itemMock.VerifyOnce(x => x.ClearFromHands(_assertableCallback));
            _assertableCallback.AssertNeverCalled();
        }

        void ArrangeItemLocatorReturnsItem()
        {
            _itemLocatorMock
                .SetupSome(x => x.GetItemByName(It.IsAny<string>()), _itemMock.Object);
        }
    }
}