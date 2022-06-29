using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ProjectEuler.Common;

namespace ProjectEuler
{
    [TestFixture]
    public class Solutions
    {
        private PrimeCache _primeCache;
    
        [OneTimeSetUp]
        public void Setup()
        {
            _primeCache = new PrimeCache();
        }
        
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
            var answer = TestHelpers.PrimeFactors(value, _primeCache).Last();

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
                        .PrimeFactors(i, _primeCache)
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
        
        [TestCase(10, 2640)]
        [TestCase(100, 25164150)]
        public void Problem6(int max, int expected)
        {
            var sumOfSquares = Enumerable.Range(1, max)
                .Select(x => x * x)
                .Sum();

            var squareOfSum = Enumerable.Range(1, max).Sum();
            squareOfSum *= squareOfSum;

            var answer = squareOfSum - sumOfSquares;
            
            Assert.That(answer, Is.EqualTo(expected));
        }
        
        [TestCase(6, 13)]
        [TestCase(10001, 104743)]
        public void Problem7(int n, int expected)
        {
            var primes = Array.Empty<long>();
            var maxPrime = 10000;
            while (primes.Length < n)
            {
                primes = _primeCache.GetPrimes(maxPrime).ToArray();
                maxPrime *= 10;
            }

            var answer = primes[n - 1];
                
            Assert.That(answer, Is.EqualTo(expected));
        }

        [TestCase(4, 5832)]
        [TestCase(13, 23514624000)]
        public void Problem8(int max, long expected)
        {
            var input = @"73167176531330624919225119674426574742355349194934
96983520312774506326239578318016984801869478851843
85861560789112949495459501737958331952853208805511
12540698747158523863050715693290963295227443043557
66896648950445244523161731856403098711121722383113
62229893423380308135336276614282806444486645238749
30358907296290491560440772390713810515859307960866
70172427121883998797908792274921901699720888093776
65727333001053367881220235421809751254540594752243
52584907711670556013604839586446706324415722155397
53697817977846174064955149290862569321978468622482
83972241375657056057490261407972968652414535100474
82166370484403199890008895243450658541227588666881
16427171479924442928230863465674813919123162824586
17866458359124566529476545682848912883142607690042
24219022671055626321111109370544217506941658960408
07198403850962455444362981230987879927244284909188
84580156166097919133875499200524063689912560717606
05886116467109405077541002256983155200055935729725
71636269561882670428252483600823257530420752963450";

            var digits = input.Select(c => 
                long.TryParse(c.ToString(), out long d) ? d : -1)
                    .Where(d => d != -1)
                    .ToArray();

            var answer = Enumerable.Range(0, digits.Length - max + 1)
                .Select(i =>
                    digits
                        .Skip(i)
                        .Take(max)
                        .Aggregate((long) 1, (x, y) => x * y))
                .Max();

            Assert.That(answer, Is.EqualTo(expected));
        }
        
        [Test]
        public void Problem9()
        {
            var cMax = 1000;
            var bMax = 1000 / 2;
            var aMax = 1000 / 3;

            var triple = Enumerable.Range(0, aMax)
                .SelectMany(i => Enumerable.Range(i + 1, bMax)
                    .SelectMany(j => Enumerable.Range(j + 1, cMax)
                        .Select(k => (i, j, k))))
                .Where(ijk => ijk.i + ijk.j + ijk.k == 1000)
                .Single(ijk => ijk.i * ijk.i + ijk.j * ijk.j == ijk.k * ijk.k);
            
            var answer = triple.i * triple.j * triple.k;
            
            Assert.That(answer, Is.EqualTo(31875000));
        }
        
        [TestCase(10, 17)]
        [TestCase(2_000_000, 142913828922)]
        public void Problem10(int max, long expected)
        {
            var answer = _primeCache.GetPrimes(max).Sum();
            
            Assert.That(answer, Is.EqualTo(expected));
        }
    }
}