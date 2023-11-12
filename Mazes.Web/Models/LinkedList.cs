using System.Collections;

namespace Mazes.Web.Models
{
    public class LinkedList<T> : IEnumerable<T>
    {
        public readonly T Value;
        public readonly LinkedList<T> Previous;
        public readonly int Length;

        public LinkedList(T value, LinkedList<T> previous = null)
        {
            Value = value;
            Previous = previous;
            Length = previous?.Length + 1 ?? 1;
        }

        public IEnumerator<T> GetEnumerator()
        {
            yield return Value;
            var pathItem = Previous;
            while (pathItem != null)
            {
                yield return pathItem.Value;
                pathItem = pathItem.Previous;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
