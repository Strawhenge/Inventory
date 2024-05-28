namespace Strawhenge.Inventory.Effects
{
    public abstract class Effect
    {
        public bool IsApplied { get; private set; }

        public void Apply()
        {
            if (IsApplied) return;
            IsApplied = true;
            PerformApply();
        }

        public void Revert()
        {
            if (!IsApplied) return;
            IsApplied = false;
            PerformRevert();
        }

        protected abstract void PerformApply();

        protected abstract void PerformRevert();
    }
}