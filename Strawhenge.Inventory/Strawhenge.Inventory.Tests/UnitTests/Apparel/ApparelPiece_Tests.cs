using Moq;
using Strawhenge.Inventory.Apparel;
using Xunit;

namespace Strawhenge.Inventory.Tests.UnitTests.Apparel
{
    public class ApparelPiece_Tests
    {
        readonly ApparelSlot _slot;
        readonly Mock<IApparelView> _viewMock;
        readonly ApparelPiece _sut;

        public ApparelPiece_Tests()
        {
            _slot = new ApparelSlot("Slot 1");
            _viewMock = new Mock<IApparelView>();
            _sut = new ApparelPiece("apparel piece", _slot, _viewMock.Object);
        }

        [Fact]
        public void Equip_ShouldBeEquipped()
        {
            _sut.Equip();

            Assert.True(_sut.IsEquipped);
        }

        [Fact]
        public void Equip_WhenUnequipped_ShouldCallView()
        {
            for (int i = 0; i < 3; i++)
                _sut.Equip();

            _viewMock.Verify(x => x.Equip(), Times.Once);
        }

        [Fact]
        public void Unequip_WhenEquipped_ShouldBeUnequipped()
        {
            _sut.Equip();
            _sut.Unequip();

            Assert.False(_sut.IsEquipped);
        }

        [Fact]
        public void Unequip_WhenEquipped_ShouldCallView()
        {
            _sut.Unequip();
            _sut.Equip();

            _viewMock.Verify(x => x.Unequip(), Times.Never);

            for (int i = 0; i < 3; i++)
                _sut.Unequip();

            _viewMock.Verify(x => x.Unequip(), Times.Once);
        }

        [Fact]
        public void Equip_ThenEquipAnotherPieceWithSameSlot_FirstPieceShouldbeUnbequipped()
        {
            _sut.Equip();

            EquipNewPieceWithSameSlot();

            Assert.False(_sut.IsEquipped);
        }

        [Fact]
        public void Equip_ThenEquipAnotherPieceWithSameSlot_FirstPieceShouldCallViewUnequip()
        {
            _sut.Equip();

            EquipNewPieceWithSameSlot();

            _viewMock.Verify(x => x.Unequip());
        }

        void EquipNewPieceWithSameSlot()
        {
            var newPiece = new ApparelPiece("apparel piece 2", _slot, new Mock<IApparelView>().Object);
            newPiece.Equip();
        }
    }
}
