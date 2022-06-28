using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ProjectEuler.UnitTests
{
    [TestFixture]
    public static class TestHelpers
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
        public static IEnumerable<long> PrimeFactors(long value)
        {
            if (value <= 0)
                throw new ArgumentException("Value must be greater than zero.");
            
            var currentValue = value;
            long currentPrime = 2;
            var primes = Array.Empty<long>();

            while (true)
            {
                if (currentValue <= currentPrime)
                {
                    yield return currentValue;
                    yield break;
                }
                
                if (currentValue % currentPrime == 0)
                {
                    yield return currentPrime;
                    currentValue /= currentPrime;
                }
                else
                {
                    primes = AddNextPrime(primes);
                    currentPrime = primes.Last();
                }
            }
        }

        /// <summary>
        /// Given sequence of primes, generate next prime
        /// </summary>
        public static long[] AddNextPrime(long[] primes)
        {
            if (primes.Length == 0)
                return primes.Append(2).ToArray();

            var currentPrime = primes.Last();
            var nextPrime = currentPrime + 1;
            while (true)
            {
                var maxPossiblePrime = (long) Math.Sqrt(nextPrime);

                bool isPrime = true;
                foreach (var p in primes)
                {
                    if (p > maxPossiblePrime)
                        break;

                    if (nextPrime % p == 0)
                        isPrime = false;
                }

                if (isPrime)
                {
                    currentPrime = nextPrime;
                    return primes.Append(currentPrime).ToArray();
                }
            
                ++nextPrime;
            }
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