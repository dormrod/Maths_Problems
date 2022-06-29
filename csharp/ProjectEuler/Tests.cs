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
        public void PrimeCache_Generates100PrimesSuccessfully_InAnyOrder(long[] maxValues)
        {
            var expected = new long[] {2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97};

            var sut = new PrimeCache();
            foreach (var maxValue in maxValues)
            {
                var _ = sut.GetPrimes(maxValue).ToArray();
            }

            var actual = sut.CurrentPrimes;
            
            Assert.That(actual, Is.EquivalentTo(expected));
        }
        
        [TestCase(100_000)]
        [TestCase(1_000_000)]
        [TestCase(10_000_000)]
        [TestCase(100_000_000)]
        public void PrimeCache_GeneratesLargeNumberOfPrimesSuccessfully_WithoutOutOfMemoryException(long maxValue)
        {
            var sut = new PrimeCache();

            Assert.DoesNotThrow(() => sut.GetPrimes(maxValue).ToArray());
        }
    }
}