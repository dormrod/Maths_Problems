using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProjectEuler.Common
{
    /// <summary>
    /// Store of natural numbers as both values and collections of digits.
    /// </summary>
    public class NaturalNumber
    {
        private static readonly Regex NumericPattern = new Regex(@"^\d+$");
        
        private readonly long? _value;
        private int[] _digits;
        
        public NaturalNumber(long value)
        {
            if (value < 0)
                throw new ArgumentException("Value must be greater or equal to zero.");
            
            _value = value;
        }
        
        public NaturalNumber(string value)
        {
            if (!NumericPattern.IsMatch(value))
                throw new ArgumentException("Value must only contain numeric digits.");

            var valueNoLeadingZeros = value.TrimStart('0');

            valueNoLeadingZeros = !string.IsNullOrEmpty(valueNoLeadingZeros) ? valueNoLeadingZeros : "0";
            
            _digits = valueNoLeadingZeros
                .Reverse()
                .Select(c => Convert.ToInt32(c.ToString()))
                .ToArray();
            
            var doesParse = long.TryParse(valueNoLeadingZeros, out var parsedValue);

            _value = doesParse ? parsedValue : (long?) null;
        }

        public NaturalNumber Add(NaturalNumber number)
        {
            var thisDigits = GetDigits().ToArray();
            var otherDigits = number.GetDigits().ToArray();
            var summedDigits = new int[Math.Max(thisDigits.Length, otherDigits.Length) + 1];

            for (int i = 0; i < thisDigits.Length; ++i)
            {
                summedDigits[i] += thisDigits[i];
            }
            
            for (int i = 0; i < otherDigits.Length; ++i)
            {
                summedDigits[i] += otherDigits[i];
            }

            for (int i = 0; i < summedDigits.Length - 1; ++i)
            {
                var quotient = summedDigits[i] / 10;
                var remainder = summedDigits[i] % 10;
                
                summedDigits[i] = remainder;
                summedDigits[i + 1] += quotient;
            }

            var value = string.Join(string.Empty, summedDigits.Reverse().Select(d => d.ToString()));

            return new NaturalNumber(value);
        }
        
        public long Value
            => _value ?? throw new InvalidCastException("Value is too large to be represented as an integer.");

        public IEnumerable<int> GetDigits(int @base = 10)
        {
            var isBase10 = @base == 10;
            
            if (_digits != null && isBase10)
                return _digits;

            var remainder = Value;
            var digits = new List<int>();
            
            while (true)
            {
                var digit = (int) (remainder % @base);
                remainder = (remainder - digit) / @base;
                digits.Add(digit);
                
                if (remainder == 0)
                    break;
            }

            // Cache digits
            if (isBase10)
                _digits = digits.ToArray();

            return digits;
        }
        
        public IEnumerable<long> GetPrimeFactors(PrimeCache primeCache)
        {
            if (Value == 0)
                return Enumerable.Empty<long>();
            
            var factors = new List<long>();
            
            // Don't want to just fetch all the primes up to the value, as this may be an enormous number!
            var currentValue = Value;
            var maxPrime = Math.Min(Value, 1000);
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
            if (Value == 0)
                return Enumerable.Empty<long>();
            
            var factors = new List<long> {1};

            if (Value == 1)
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