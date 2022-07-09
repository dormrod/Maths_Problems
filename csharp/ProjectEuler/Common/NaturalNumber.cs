using System.Collections.Generic;

namespace ProjectEuler.Common
{
    public class NaturalNumber
    {
        private readonly long _value;

        public NaturalNumber(long value)
        {
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
    }
}