namespace Budgethold.Common.Extensions
{
    public static class IHashSetExtensions
    {
        public static void AddRange<T>(this HashSet<T> source, IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                source.Add(item);
            }
        }
    }
}
