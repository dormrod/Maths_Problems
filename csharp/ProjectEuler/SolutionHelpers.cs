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
        
        /// <summary>
        /// Generate Prime factors for value.
        /// </summary>
        public static IEnumerable<long> PrimeFactors(long value, PrimeCache primeCache)
        {
            if (value <= 0)
                throw new ArgumentException("Value must be greater than zero.");
            
            var factors = new List<long>();
            
            // Don't want to just fetch all the primes up to the value, as this may be an enormous number!
            var currentValue = value;
            var maxPrime = Math.Min(value, 1000);
            while (currentValue != 1)
            {
                var primes = primeCache.GetPrimes(maxPrime);
                foreach (var prime in primes)
                {
                    while (currentValue % prime == 0)
                    {
                        factors.Add(prime);
                        currentValue /= prime;
                    }

                    if (currentValue == 1)
                        break;
                }

                maxPrime *= 10;
            }

            return factors;
        }

        /// <summary>
        /// Convert integer to an array of composite digits,
        /// where the array index corresponds to the nth power of 10.
        /// </summary>
        public static IEnumerable<int> ToDigits(int value)
        {
            var remainder = value;
            var digits = new List<int>();
            
            while (true)
            {
                var digit = remainder % 10;
                remainder = (remainder - digit) / 10;
                digits.Add(digit);
                
                if (remainder == 0)
                    break;
            }

            return digits;
        }
    }
}