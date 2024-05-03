namespace Strawhenge.Inventory.Apparel
{
    public interface IApparelPiece
    {
        string Name { get; }

        string SlotName { get; }

        bool IsEquipped { get; }

        UnequipPreference UnequipPreference { set; }

        void Equip();

        void Unequip();
    }
}