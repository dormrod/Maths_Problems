using System;
using System.Collections.Generic;
using ProjectEuler.Common;

namespace ProjectEuler
{
    public static class SolutionHelpers
    {
        /// <summary>
        /// Generate Fibonacci sequence with values less than the provided cutoff.
        /// </summary>
        public static IEnumerable<(int Index, int Value)> FibonacciSequence(int cutoff = 10)
        {
            var firstTerm = 0;
            var secondTerm = 1;
            var i = 0;

            yield return (++i, firstTerm);
            yield return (++i, secondTerm);
            
            while (true)
            {
                var thirdTerm = firstTerm + secondTerm;
                
                if (thirdTerm > cutoff)
                    yield break;

                yield return (i++, thirdTerm);

                firstTerm = secondTerm;
                secondTerm = thirdTerm;
            }
        }
    }
}