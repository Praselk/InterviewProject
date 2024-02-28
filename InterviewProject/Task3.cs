using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProject.Task3
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<TSource> MyWhere<TSource>(this IEnumerable<TSource> sources, Func<TSource, bool> predicate)
        {
            foreach (TSource source in sources)
            {
                if (predicate != null && predicate(source))
                    yield return source;
            }
        }
    }
}
