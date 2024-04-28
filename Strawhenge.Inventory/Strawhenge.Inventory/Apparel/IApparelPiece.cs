namespace Strawhenge.Inventory.Apparel
{
    public interface IApparelPiece
    {
        string Name { get; }
      
        string SlotName { get; }
      
        bool IsEquipped { get; }
      
        void Equip();
       
        void Unequip();
    }
}