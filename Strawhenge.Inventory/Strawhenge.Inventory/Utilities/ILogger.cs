namespace Strawhenge.Inventory
{
    public interface ILogger
    {
        void LogWarning(string message);

        void LogError(string message);
    }
}
