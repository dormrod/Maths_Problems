using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Common
{
    /// <summary>
    /// Generates ordered arrays of Fibonacci numbers, up to a given value.
    /// </summary>
    /// <remarks>
    /// Not thread safe.
    /// </remarks>
    public sealed class FibonacciGenerator : ISequenceGenerator
    {
        public FibonacciGenerator()
        {
        }

        public long NextValue(long currentValue)
            => GetValues(currentValue).Reverse().Take(2).Sum();

        public IEnumerable<long> GetValues(long maxValue)
        {
            var previousValue = 1;
            var currentValue = 2;

            yield return previousValue;
            yield return currentValue;
            
            while (true)
            {
                var nextValue = previousValue + currentValue;
                
                if (nextValue > maxValue)
                    yield break;

                yield return nextValue;

                previousValue = currentValue;
                currentValue = nextValue;
            }
        }
    }
}