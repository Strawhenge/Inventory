using Moq;
using Strawhenge.Inventory.TransientItems;
using Xunit;

namespace Strawhenge.Inventory.Tests.UnitTests
{
    public class TransientItemHolder_Tests
    {
        readonly Mock<ITransientItemLocator> itemLocatorMock;
        readonly Mock<IItem> itemMock;
        readonly ITransientItemHolder transientItemHolder;
        readonly AssertableCallback assertableCallback;

        public TransientItemHolder_Tests()
        {
            itemMock = new Mock<IItem>();

            itemLocatorMock = new Mock<ITransientItemLocator>();
            itemLocatorMock.SetupNone(x => x.GetItemByName(It.IsAny<string>()));

            transientItemHolder = new TransientItemHolder(itemLocatorMock.Object);

            assertableCallback = new AssertableCallback();
        }

        [Fact]
        public void HoldLeftHand_WhenItemNotLocated()
        {
            transientItemHolder.HoldLeftHand(It.IsAny<string>(), assertableCallback);

            assertableCallback.AssertCalledOnce();
        }

        [Fact]
        public void HoldLeftHand_WhenItemLocated()
        {
            ArrangeItemLocatorReturnsItem();

            transientItemHolder.HoldLeftHand(It.IsAny<string>(), assertableCallback);

            itemMock.VerifyOnce(x => x.HoldLeftHand(assertableCallback));
            assertableCallback.AssertNeverCalled();
        }

        [Fact]
        public void HoldRightHand_WhenItemNotLocated()
        {
            transientItemHolder.HoldRightHand(It.IsAny<string>(), assertableCallback);

            assertableCallback.AssertCalledOnce();
        }

        [Fact]
        public void HoldRightHand_WhenItemLocated()
        {
            ArrangeItemLocatorReturnsItem();

            transientItemHolder.HoldRightHand(It.IsAny<string>(), assertableCallback);

            itemMock.VerifyOnce(x => x.HoldRightHand(assertableCallback));
            assertableCallback.AssertNeverCalled();
        }

        [Fact]
        public void Unhold()
        {
            transientItemHolder.Unhold(assertableCallback);

            assertableCallback.AssertCalledOnce();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Unhold_WhenItemIsHeld(bool leftHand)
        {
            ArrangeItemLocatorReturnsItem();

            if (leftHand)
                transientItemHolder.HoldLeftHand(It.IsAny<string>());
            else
                transientItemHolder.HoldRightHand(It.IsAny<string>());

            transientItemHolder.Unhold(assertableCallback);

            itemMock.VerifyOnce(x => x.ClearFromHands(assertableCallback));
            assertableCallback.AssertNeverCalled();
        }

        void ArrangeItemLocatorReturnsItem()
        {
            itemLocatorMock
                .SetupSome(x => x.GetItemByName(It.IsAny<string>()), itemMock.Object);
        }
    }
}
