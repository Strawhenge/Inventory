namespace Strawhenge.Inventory
{
    public interface IDataSetter
    {
        /// <summary>
        /// Stores <paramref name="value"/> as <typeparamref name="T"/>. <c>Get&lt;T&gt;()</c> returns it, and a later
        /// call with the same <typeparamref name="T"/> replaces it.
        /// </summary>
        void Set<T>(T value) where T : class;

        /// <summary>
        /// Stores <paramref name="value"/> so that <c>Get&lt;T&gt;()</c> finds it by its own type, any of its base types
        /// or any interface it implements. Values stored with <see cref="Set{T}"/> take priority. When more than one
        /// added value matches, the first one added is returned and a warning is logged.
        /// </summary>
        void Add(object value);
    }
}