using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProject
{
    public static class IAsyncEnumerableExtensions
    {
        public static async IAsyncEnumerable<List<T>> ToBatchesAsync<T>(this IAsyncEnumerable<T> source, int batchSize, 
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var batch = new List<T>();
            await foreach (var item in source)
            {
                if (cancellationToken.IsCancellationRequested)
                    yield break;
                batch.Add(item);
                if (batch.Count >= batchSize)
                {
                    yield return batch;
                    batch = new List<T>();
                }
            }
            if (batch.Count > 0)
            {
                yield return batch;
            }
        }
    }
}
