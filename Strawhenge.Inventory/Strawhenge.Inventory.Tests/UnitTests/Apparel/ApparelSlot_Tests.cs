using Moq;
using Strawhenge.Inventory.Apparel;
using Xunit;

namespace Strawhenge.Inventory.Tests.UnitTests.Apparel
{
    public class ApparelSlot_Tests
    {
        readonly ApparelSlot _sut;
        readonly ApparelPiece _piece;

        public ApparelSlot_Tests()
        {
            _sut = new ApparelSlot("Slot 1");
            _piece = new ApparelPiece("apparel", _sut, new Mock<IApparelView>().Object);
        }

        [Fact]
        public void EquipPiece_SlotShouldHavePiece()
        {
            _piece.Equip();

            AssertMaybe.IsSome(_sut.CurrentPiece, _piece);
        }

        [Fact]
        public void UnequipPiece_SlotShouldHavePiece()
        {
            _piece.Equip();
            _piece.Unequip();

            AssertMaybe.IsNone(_sut.CurrentPiece);
        }
    }
}
