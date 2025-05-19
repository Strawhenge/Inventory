using Strawhenge.Inventory.Apparel;
using Xunit;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ApparelPieceTests
{
    public class ApparelPieceTests
    {
        const string Head = "Head";
        
        const string Hat = "Hat";
        const string Helmet = "Helmet";
        
        const string Show = "Show";
        const string Hide = "Hide";
        const string Drop = "Drop";

        readonly InventoryTestContext _context;

        readonly ApparelSlot _headSlot;

        readonly InventoryApparelPiece _hat;
        readonly InventoryApparelPiece _helmet;

        public ApparelPieceTests(ITestOutputHelper testOutputHelper)
        {
            _context = new InventoryTestContext(testOutputHelper);
            _context.AddApparelSlot(Head);

            _headSlot = (ApparelSlot)_context.Inventory.ApparelSlots[Head];

            _hat = _context.CreateApparel(Hat, Head);
            _helmet = _context.CreateApparel(Helmet, Head);
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

            _context.VerifyApparelViewCalls((Hat, Show));
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

            for (int i = 0; i < 3; i++)
                _hat.Unequip();

            _context.VerifyApparelViewCalls(
                (Hat, Show),
                (Hat, Drop));
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

            _context.VerifyApparelViewCalls(
                (Hat, Show),
                (Hat, Drop),
                (Helmet, Show));
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

            _context.VerifyApparelViewCalls(
                (Hat, Show),
                (Hat, Hide));
        }
    }
}