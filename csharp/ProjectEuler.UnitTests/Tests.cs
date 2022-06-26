using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ProjectEuler.UnitTests
{
    [TestFixture]
    public class Tests
    {
        [TestCase(10, 23)]
        [TestCase(1000, 233168)]
        public void Problem1(int cutoff, int expected)
        {
            var answer = Enumerable.Range(0, cutoff)
                .Where(i => i % 3 == 0 || i % 5 == 0)
                .Sum();

            Assert.That(answer, Is.EqualTo(expected));
        }

        [TestCase(10, 10)]
        [TestCase(4000000, 4613732)]
        public void Problem2(int cutoff, int expected)
        {
            var answer = TestHelpers.FibonacciSequence(cutoff)
                .Where(x => x.Value % 2 == 0)
                .Sum(x => x.Value);

            Assert.That(answer, Is.EqualTo(expected));
        }
        
        [TestCase(10, 5)]
        [TestCase(100001, 9091)]
        [TestCase(600851475143, 6857)]
        public void Problem3(long value, int expected)
        {
            var answer = TestHelpers.PrimeFactors(value).Last();

            Assert.That(answer, Is.EqualTo(expected));
        }

        [TestCase(99, 9009)]
        [TestCase(999, 906609)]
        public void Problem4(int max, int expected)
        {
            var answer = Enumerable.Range(1, max)
                .SelectMany(i => Enumerable.Range(i, max - i + 1).Select(j => i * j))
                .Select(v => new
                {
                    Value = v,
                    Digits = TestHelpers.ToDigits(v)
                })
                .Where(x => x.Digits
                    .Zip(
                        x.Digits.Reverse(), 
                        (i, j) => i == j)
                    .All(b => b))
                .Max(x => x.Value);

            Assert.That(answer, Is.EqualTo(expected));
        }
        
        [TestCase(10, 2520)]
        [TestCase(20, 232792560)]
        public void Problem5(int max, int expected)
        {
            var answer = Enumerable.Range(1, max)
                .SelectMany(i =>
                    TestHelpers
                        .PrimeFactors(i)
                        .Select(Convert.ToInt32)
                        .GroupBy(f => f)
                        .Select(g => new KeyValuePair<int, int>(g.Key, g.Count())))
                .GroupBy(kvp => kvp.Key)
                .Select(g => (g.Key, g.Max(x => x.Value)))
                .Aggregate(1, (x, y) =>
                    x * Enumerable.Range(0, y.Item2)
                        .Select(_ => y.Key)
                        .Aggregate(1, (m, n) => m * n));

            Assert.That(answer, Is.EqualTo(expected));
        }
        
        
    }
}