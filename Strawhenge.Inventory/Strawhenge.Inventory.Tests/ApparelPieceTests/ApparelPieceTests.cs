using Moq;
using Strawhenge.Inventory.Apparel;
using Xunit;

namespace Strawhenge.Inventory.Tests.ApparelPieceTests
{
    public class ApparelPieceTests
    {
        readonly ApparelSlot _headSlot;

        readonly ApparelPiece _hat;
        readonly Mock<IApparelView> _hatViewMock;

        readonly ApparelPiece _helmet;
        readonly Mock<IApparelView> _helmetViewMock;

        public ApparelPieceTests()
        {
            _headSlot = new ApparelSlot("Head");

            (_hat, _hatViewMock) = CreateTorsoPiece("Hat");
            (_helmet, _helmetViewMock) = CreateTorsoPiece("Helmet");
        }

        [Fact]
        public void Equip_ShouldBeEquipped()
        {
            _hat.Equip();

            Assert.True(_hat.IsEquipped);
        }

        [Fact]
        public void Equip_WhenUnequipped_ShouldCallView()
        {
            for (int i = 0; i < 3; i++)
                _hat.Equip();

            _hatViewMock.Verify(x => x.Show(), Times.Once);
        }

        [Fact]
        public void Unequip_WhenEquipped_ShouldBeUnequipped()
        {
            _hat.Equip();
            _hat.Unequip();

            Assert.False(_hat.IsEquipped);
        }

        [Fact]
        public void Unequip_WhenEquipped_ShouldCallView()
        {
            _hat.Unequip();
            _hat.Equip();

            _hatViewMock.Verify(x => x.Drop(), Times.Never);
            _hatViewMock.Verify(x => x.Hide(), Times.Never);

            for (int i = 0; i < 3; i++)
                _hat.Unequip();

            _hatViewMock.Verify(x => x.Drop(), Times.Once);
            _hatViewMock.Verify(x => x.Hide(), Times.Never);
        }

        [Fact]
        public void Equip_ThenEquipAnotherPieceWithSameSlot_FirstPieceShouldBeUnequipped()
        {
            _hat.Equip();
            _helmet.Equip();

            Assert.False(_hat.IsEquipped);
        }

        [Fact]
        public void Equip_ThenEquipAnotherPieceWithSameSlot_FirstPieceShouldCallViewDrop()
        {
            _hat.Equip();
            _helmet.Equip();

            _hatViewMock.Verify(x => x.Drop());
            _helmetViewMock.Verify(x => x.Show());
        }

        [Fact]
        public void EquipPiece_SlotShouldHavePiece()
        {
            _hat.Equip();

            _headSlot.CurrentPiece.VerifyIsSome(_hat);
        }

        [Fact]
        public void UnequipPiece_SlotShouldHavePiece()
        {
            _hat.Equip();
            _hat.Unequip();

            _headSlot.CurrentPiece.VerifyIsNone();
        }

        [Fact]
        public void DiscardPiece_ShouldNotBeEquipped()
        {
            _hat.Equip();
            _hat.Discard();

            Assert.False(_hat.IsEquipped);
        }

        [Fact]
        public void DiscardPiece_ShouldCallView()
        {
            _hat.Equip();
            _hat.Discard();

            _hatViewMock.Verify(x => x.Hide(), Times.Once);
        }

        (ApparelPiece piece, Mock<IApparelView> viewMock) CreateTorsoPiece(string name)
        {
            var viewMock = new Mock<IApparelView>();
            return (new ApparelPiece(name, _headSlot, viewMock.Object), viewMock);
        }
    }
}