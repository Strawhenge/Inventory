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

        readonly IItem _targetItem;
        readonly InventoryTestContext _context;
        int _otherItemCount;
        
        protected BaseTransientItemLocatorTest(ITestOutputHelper testOutputHelper)
        {
            _context = new InventoryTestContext(testOutputHelper);
            _context.StoredItemsWeightCapacity.SetWeightCapacity(100);
            _context.AddHolsters(Holsters);

            _targetItem = _context.CreateItem(
                TargetItemName,
                ItemSize.OneHanded,
                Holsters,
                storable: true
            );
        }

        protected IItem TargetItem => _targetItem;

        protected abstract bool GetItemByName_ShouldReturnTargetItem { get; }

        protected virtual IItem ItemInLeftHand => null;

        protected virtual IItem ItemInRightHand => null;

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

        protected virtual IEnumerable<(string holsterName, IItem item)> ItemsInHolsters() =>
            Enumerable.Empty<(string holsterName, IItem item)>();

        protected virtual IEnumerable<IItem> ItemsInStorage() =>
            Enumerable.Empty<IItem>();

        protected virtual IItem GenerateItem() => null;

        protected IItem NonTargetItem(string name = null)
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
            if (ItemInLeftHand is IItem left)
                left.HoldLeftHand();

            if (ItemInRightHand is IItem right)
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

            if (GenerateItem() is IItem generatedItem)
                _context.SetGeneratedItem(TargetItemName, generatedItem);
        }
    }
}