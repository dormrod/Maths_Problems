using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Common
{
    /// <summary>
    /// Cache for generating ordered arrays of prime numbers, up to a given value.
    /// </summary>
    /// <remarks>
    /// Not thread safe.
    /// </remarks>
    public class PrimeCache
    {
        private long[] _primes;

        public long[] CurrentPrimes => _primes;
        
        public PrimeCache(long max = 2)
        {
            _primes = Array.Empty<long>();
            GeneratePrimes(max);
        }

        public IEnumerable<long> GetPrimes(long max)
        {
            if (max > _primes.LastOrDefault())
                GeneratePrimes(max);
            
            // The primes must be sorted so can break as soon as the limit is reached
            foreach (var prime in _primes)
            {
                if (prime > max)
                    yield break;

                yield return prime;
            }
        }

        private void GeneratePrimes(long max)
        {
            // First prime is 2
            if (max <= 1)
                throw new ArgumentException("Maximum prime value must be greater than 1");
            
            // Only want to check the next N values for primes
            var startValue = Math.Max(_primes.LastOrDefault() + 1, 2);
            var endValue = max;
            var numberOfValues = endValue - startValue + 1;
            
            // Sieve of Eratosthenes, set multiples of primes to false
            var isPrime = new bool[numberOfValues];
            for (long i = 0; i < numberOfValues; ++i)
            {
                isPrime[i] = true;
            }
            
            // First check primes in cache, taking first value as closest multiple
            foreach (long prime in _primes)
            {
                long j = (long) Math.Ceiling((decimal) startValue / prime) * prime - startValue;
                while (j < numberOfValues)
                {
                    isPrime[j] = false;
                    j += prime;
                }
            }

            // Then check new numbers sequentially
            var currentValue = startValue;
            for (long i = 0; i < numberOfValues; ++i)
            {
                if (!isPrime[i])
                    continue;
                
                long j = (long) Math.Ceiling((decimal) startValue / currentValue) * currentValue * 2 - startValue;
                while (j < numberOfValues)
                {
                    if (j < 0)
                        continue;
                    
                    isPrime[j] = false;
                    j += currentValue;
                }

                ++currentValue;
            }

            // Add values to cache which have no prime factors
            var value = startValue;
            var additionalPrimes = new List<long>();
            foreach (var valueIsPrime in isPrime)
            {
                if (valueIsPrime) 
                    additionalPrimes.Add(value);
                    
                ++value;
            }

            _primes = _primes.Concat(additionalPrimes).ToArray();
        }
    }
}