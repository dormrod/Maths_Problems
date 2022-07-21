using System;
using System.Linq;
using NUnit.Framework;
using ProjectEuler.Common;

namespace ProjectEuler
{
    [TestFixture]
    public class Tests
    {
        [TestCase(new long[] {100})]
        [TestCase(new long[] {2, 100})]
        [TestCase(new long[] {2, 5, 10, 97, 100})]
        [TestCase(new long[] {2, 5, 10, 32, 41, 42, 97, 100})]
        [TestCase(new long[] {2, 2, 5, 4, 10, 32, 41, 42, 17, 17, 97, 100})]
        public void PrimeGenerator_GeneratesPrimesUpTo100_Successfully(long[] maxValues)
        {
            var expected = new long[] {2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97};

            var sut = new PrimeGenerator();
            foreach (var maxValue in maxValues)
            {
                var _  = sut.GetValues(maxValue).ToArray();
            }

            var actual = sut.GetCache();
            
            Assert.That(actual, Is.EquivalentTo(expected));
        }
        
        [TestCase(100_000)]
        [TestCase(1_000_000)]
        [TestCase(10_000_000)]
        [TestCase(100_000_000, Explicit = true, Reason = "Relatively long running (<1m)")]
        public void PrimeGenerator_GeneratesLargeNumberOfPrimes_SuccessfullyWithoutOutOfMemoryException(long maxValue)
        {
            var sut = new PrimeGenerator();

            Assert.DoesNotThrow(() => sut.GetValues(maxValue).ToArray());
        }
        
        [TestCase(2)]
        [TestCase(41)]
        [TestCase(1000)]
        
        public void PrimeGenerator_GeneratesNextThreePrimesAfter100_Successfully(long startingValue)
        {
            var expected = new long[] {101, 103, 107};
            
            var sut = new PrimeGenerator(startingValue);

            var next1 = sut.NextValue(100);
            var next2 = sut.NextValue(next1);
            var next3 = sut.NextValue(next2);

            var actual = new[] {next1, next2, next3};
            
            Assert.That(actual, Is.EquivalentTo(expected));
        }
        
        [TestCase(100_000)]
        [TestCase(1_000_000)]
        [TestCase(10_000_000)]
        [TestCase(100_000_000, Explicit = true, Reason = "Relatively long running (<1m)")]
        public void PrimeGenerator_GeneratesNextPrimeAfterALargeNumberOfPrimes_SuccessfullyWithoutOutOfMemoryException(long maxValue)
        {
            var sut = new PrimeGenerator();

            Assert.DoesNotThrow(() => sut.NextValue(maxValue));
        }
        
        [Test]
        public void FibonacciGenerator_GeneratesSequenceUpTo100_Successfully()
        {
            var expected = new long[] {1, 2, 3, 5, 8, 13, 21, 34, 55, 89};
            
            var sut = new FibonacciGenerator();

            var actual = sut.GetValues(100);

            Assert.That(actual, Is.EquivalentTo(expected));
        }
        
        [TestCase(54, 55)]
        [TestCase(55, 89)]
        [TestCase(56, 89)]
        public void FibonacciGenerator_GeneratesNextNumberInSequence_Successfully(long current, long expected)
        {
            var sut = new FibonacciGenerator();

            var actual = sut.NextValue(current);

            Assert.That(actual, Is.EqualTo(expected));
        }
        
        [TestCase(1, new long[] {1})]
        [TestCase(13, new long[] {13, 40, 20, 10, 5, 16, 8, 4 , 2, 1})]
        public void CollatzGenerator_GeneratesSequences_Successfully(long value, long[] expected)
        {
            var sut = new CollatzGenerator();

            var actual = sut.GetValues(value);

            Assert.That(actual, Is.EquivalentTo(expected));
        }
        
        [TestCase(1, 4)]
        [TestCase(2, 1)]
        [TestCase(3, 10)]
        [TestCase(4, 2)]
        public void CollatzGenerator_GeneratesNextNumberInSequence_Successfully(long current, long expected)
        {
            var sut = new CollatzGenerator();

            var actual = sut.NextValue(current);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(100)]
        [TestCase(1_000_000)]
        [TestCase(1_000_000_000_000)]
        public void NaturalNumber_CanBeInitialisedWithNaturalValue_Successfully(long value)
            => Assert.DoesNotThrow(() => new NaturalNumber(value));
        
        [TestCase(-1)]
        public void NaturalNumber_WhenInitialisedWithNegativeValue_Throws(long value)
            => Assert.Throws<ArgumentException>(() => new NaturalNumber(value));

        [TestCase("0",new [] {0}, (long) 0)]
        [TestCase("1", new [] {1}, (long) 1)]
        [TestCase("2", new [] {2}, (long) 2)]
        [TestCase("100", new [] {0, 0, 1}, (long) 100)]
        [TestCase("00123", new [] {3, 2, 1}, (long) 123)]
        [TestCase("1000000", new [] {0, 0, 0, 0, 0, 0, 1}, (long) 1_000_000)]
        [TestCase("1000000000123", new [] {3, 2, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 1_000_000_000_123)]
        [TestCase("1000000000000000000123", new [] {3, 2, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, null)]
        public void NaturalNumber_CanBeInitialisedWithStringValue_Successfully(string value, int[] expectedDigits, long? expectedValue)
        {
            var sut = new NaturalNumber(value);
            
            Assert.That(sut.GetDigits(), Is.EqualTo(expectedDigits));
            if (expectedValue != null)
            {
                Assert.That(sut.Value, Is.EqualTo(expectedValue));
            }
            else
            {
                Assert.Throws<InvalidCastException>(() =>
                {
                    _= sut.Value;
                });
            }
        }
        
        [TestCase("123.345")]
        [TestCase("123a")]
        public void NaturalNumber_WhenInitialisedWithInvalidStringValue_Throws(string value)
            => Assert.Throws<ArgumentException>(() => new NaturalNumber(value));
        
        [TestCase(0, new long[0])]
        [TestCase(1, new long[0])]
        [TestCase(2, new long[] {2})]
        [TestCase(10, new long[] {2, 5})]
        [TestCase(43, new long[] {43})]
        [TestCase(100, new long[] {2, 2, 5, 5})]
        [TestCase(1_000_001, new long[] {101, 9901})]
        public void NaturalNumber_CanGetPrimeFactors_Successfully(long value, long[] expected)
        {
            var primeGenerator = new PrimeGenerator();
            
            var sut = new NaturalNumber(value);
            
            Assert.That(sut.GetPrimeFactors(primeGenerator).ToArray(), Is.EqualTo(expected));
        }
        
        [TestCase(0, new long[0])]
        [TestCase(1, new long[] {1})]
        [TestCase(2, new long[] {1, 2})]
        [TestCase(10, new long[] {1, 2, 5, 10})]
        [TestCase(28, new long[] {1, 2, 4, 7, 14, 28})]
        [TestCase(43, new long[] {1, 43})]
        [TestCase(100, new long[] {1, 2, 4, 5, 10, 20, 25, 50, 100})]
        [TestCase(1000001, new long[] {1, 101, 9901, 1000001})]
        public void NaturalNumber_CanGetFactors_Successfully(long value, long[] expected)
        {
            var primeGenerator = new PrimeGenerator();
            
            var sut = new NaturalNumber(value);
            
            Assert.That(sut.GetFactors(primeGenerator).ToArray(), Is.EqualTo(expected));
        }
        
        [TestCase(0, 0, 0)] 
        [TestCase(1, 2, 3)] 
        [TestCase(2, 1, 3)] 
        [TestCase(199, 99, 298)] 
        [TestCase(1_000_001, 99, 1_000_100)] 
        public void NaturalNumber_CanSumValues_Successfully(long value1, long value2, long expected)
        {
            var sut = new NaturalNumber(value1);

            var actual = sut.Add(new NaturalNumber(value2));
            
            Assert.That(actual.Value, Is.EqualTo(expected));
        }
    }
}