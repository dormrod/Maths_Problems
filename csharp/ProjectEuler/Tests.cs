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
        public void PrimeCache_GeneratesPrimesUpTo100_Successfully(long[] maxValues)
        {
            var expected = new long[] {2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97};

            var sut = new PrimeCache();
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
        public void PrimeCache_GeneratesLargeNumberOfPrimes_SuccessfullyWithoutOutOfMemoryException(long maxValue)
        {
            var sut = new PrimeCache();

            Assert.DoesNotThrow(() => sut.GetValues(maxValue).ToArray());
        }
        
        [TestCase(2)]
        [TestCase(41)]
        [TestCase(1000)]
        
        public void PrimeCache_GeneratesNextThreePrimesAfter100_Successfully(long startingValue)
        {
            var expected = new long[] {101, 103, 107};
            
            var sut = new PrimeCache(startingValue);

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
        public void PrimeCache_GeneratesNextPrimeAfterALargeNumberOfPrimes_SuccessfullyWithoutOutOfMemoryException(long maxValue)
        {
            var sut = new PrimeCache();

            Assert.DoesNotThrow(() => sut.NextValue(maxValue));
        }
    }
}