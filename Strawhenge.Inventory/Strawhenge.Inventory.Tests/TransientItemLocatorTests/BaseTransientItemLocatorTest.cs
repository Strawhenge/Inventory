using System.Collections.Generic;
using System.Linq;
using Strawhenge.Inventory.Items;
using Xunit;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.TransientItemLocatorTests
{
    public abstract class BaseTransientItemLocatorTest
    {
        protected const string LeftHipHolster = "Left Hip";
        protected const string RightHipHolster = "Right Hip";
        protected const string BackHolster = "Back";

        static readonly string[] Holsters =
        {
            LeftHipHolster, RightHipHolster, BackHolster
        };

        const string TargetItemName = "Hammer";

        readonly Item _targetItem;
        readonly InventoryTestContext _context;
        int _otherItemCount;
        
        protected BaseTransientItemLocatorTest(ITestOutputHelper testOutputHelper)
        {
            _context = new InventoryTestContext(testOutputHelper);
            _context.SetStorageCapacity(100);
            _context.AddHolsters(Holsters);

            _targetItem = _context.CreateItem(
                TargetItemName,
                ItemSize.OneHanded,
                Holsters,
                storable: true
            );
        }

        protected Item TargetItem => _targetItem;

        protected abstract bool GetItemByName_ShouldReturnTargetItem { get; }

        protected virtual Item ItemInLeftHand => null;

        protected virtual Item ItemInRightHand => null;

        protected virtual ClearFromHandsPreference ExpectedClearFromHandsPreference => null;

        [Fact]
        public void GetItemByName()
        {
            Arrange();

            var result = _context.TransientItemLocator.GetItemByName(TargetItemName);

            if (!GetItemByName_ShouldReturnTargetItem)
            {
                result.VerifyIsNone();
                return;
            }

            result.VerifyIsSome(_targetItem);

            if (ExpectedClearFromHandsPreference is ClearFromHandsPreference expected)
            {
                Assert.IsType(expected.GetType(), _targetItem.ClearFromHandsPreference);
            }
        }

        protected virtual IEnumerable<(string holsterName, Item item)> ItemsInHolsters() =>
            Enumerable.Empty<(string holsterName, Item item)>();

        protected virtual IEnumerable<Item> ItemsInStorage() =>
            Enumerable.Empty<Item>();

        protected virtual Item GenerateItem() => null;

        protected Item NonTargetItem(string name = null)
        {
            return _context.CreateItem(
                name ?? $"Other Item {_otherItemCount++}",
                ItemSize.OneHanded,
                Holsters,
                storable: true
            );
        }

        void Arrange()
        {
            if (ItemInLeftHand is Item left)
                left.HoldLeftHand();

            if (ItemInRightHand is Item right)
                right.HoldRightHand();

            foreach (var (holsterName, item) in ItemsInHolsters())
            {
                item.Holsters[holsterName]
                    .Reduce(() => throw new TestSetupException($"'{item.Name}' not assignable to '{holsterName}'."))
                    .Equip();
            }

            foreach (var item in ItemsInStorage())
            {
                item.Storable
                    .Reduce(() => throw new TestSetupException($"'{item.Name}' is not storable."))
                    .AddToStorage();
            }

            if (GenerateItem() is Item generatedItem)
                _context.SetGeneratedItem(TargetItemName, generatedItem);
        }
    }
}