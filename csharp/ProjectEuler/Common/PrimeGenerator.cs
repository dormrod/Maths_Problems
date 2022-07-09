using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Common
{
    /// <summary>
    /// Generates and caches ordered arrays of prime numbers, up to a given value.
    /// </summary>
    /// <remarks>
    /// Not thread safe.
    /// </remarks>
    public sealed class PrimeGenerator : CachedSequenceGenerator
    {
        public PrimeGenerator(long maxValue = 2)
            => GenerateSequence(maxValue);

        protected override void GenerateNextValue()
        {
            // Generate sequence for odd numbers until find another prime
            var startLength = Sequence.LongLength;
            var value = Sequence.Last() + 2;
            while (startLength == Sequence.Length)
            {
                GenerateSequence(value);
                value += 2;
            }
        }
        
        protected override void GenerateSequence(long maxValue)
        {
            // First prime is 2
            if (maxValue <= 1)
                throw new ArgumentException("Maximum prime value must be greater than 1");
            
            // Only want to check the next N sequence for primes
            var startValue = Math.Max(Sequence.LastOrDefault() + 1, 2);
            var endValue = maxValue;
            var numberOfValues = endValue - startValue + 1;
            
            // Sieve of Eratosthenes, set multiples of primes to false
            var isPrime = new bool[numberOfValues];
            for (long i = 0; i < numberOfValues; ++i)
            {
                isPrime[i] = true;
            }
            
            // First check primes in cache, taking first value as closest multiple
            foreach (long prime in Sequence)
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

            // Add sequence to cache which have no prime factors
            var value = startValue;
            var additionalPrimes = new List<long>();
            foreach (var valueIsPrime in isPrime)
            {
                if (valueIsPrime) 
                    additionalPrimes.Add(value);
                    
                ++value;
            }

            Sequence = Sequence.Concat(additionalPrimes).ToArray();
        }
    }
}