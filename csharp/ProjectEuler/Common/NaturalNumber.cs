﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Common
{
    public class NaturalNumber
    {
        private readonly long _value;

        public NaturalNumber(long value)
        {
            if (value < 0)
                throw new ArgumentException("Value must be greater or equal to zero.");
            
            _value = value;
        }

        public long GetValue() => _value;
        
        public IEnumerable<int> GetDigits(int @base = 10)
        {
            var remainder = _value;
            var digits = new List<int>();
            
            while (true)
            {
                var digit = (int) (remainder % @base);
                remainder = (remainder - digit) / @base;
                digits.Add(digit);
                
                if (remainder == 0)
                    break;
            }

            return digits;
        }
        
        public IEnumerable<long> GetPrimeFactors(PrimeCache primeCache)
        {
            if (_value == 0)
                return Enumerable.Empty<long>();
            
            var factors = new List<long>();
            
            // Don't want to just fetch all the primes up to the value, as this may be an enormous number!
            var currentValue = _value;
            var maxPrime = Math.Min(_value, 1000);
            while (currentValue != 1)
            {
                var primes = primeCache.GetValues(maxPrime);
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

        public IEnumerable<long> GetFactors(PrimeCache primeCache)
        {
            if (_value == 0)
                return Enumerable.Empty<long>();
            
            var factors = new List<long> {1};

            if (_value == 1)
                return factors;
            
            // Get prime factors then multiply by all combinations of 0 and 1
            var primeFactors = GetPrimeFactors(primeCache).ToArray();
            
            for (var i = 0; i < Math.Pow(2, primeFactors.Length); ++i)
            {
                long factor = 1;
                var mask = new NaturalNumber(i).GetDigits(2).ToArray();
                for (var j = 0; j < mask.Length; ++j)
                {
                    factor *= Math.Max(primeFactors[j] * mask[j], 1);
                }
                
                factors.Add(factor); 
            }

            return factors.Distinct();
        }
    }
}