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
            var answer = SolutionHelpers.FibonacciSequence(cutoff)
                .Where(x => x.Value % 2 == 0)
                .Sum(x => x.Value);

            Assert.That(answer, Is.EqualTo(expected));
        }
        
        [TestCase(10, 5)]
        [TestCase(100001, 9091)]
        [TestCase(600851475143, 6857)]
        public void Problem3(long value, int expected)
        {
            var answer = SolutionHelpers.PrimeFactors(value, _primeCache).Last();

            Assert.That(answer, Is.EqualTo(expected));
        }

        [TestCase(99, 9009)]
        [TestCase(999, 906609)]
        public void Problem4(int max, int expected)
        {
            var answer = Enumerable.Range(1, max)
                .SelectMany(i => Enumerable.Range(i, max - i + 1).Select(j => i * j))
                .Select(v => new NaturalNumber(v))
                .Select(n => new
                {
                    Value = n.GetValue(),
                    Digits = n.GetDigits().ToArray()
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
                    SolutionHelpers
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
                primes = _primeCache.GetValues(maxPrime).ToArray();
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
            var answer = _primeCache.GetValues(max).Sum();
            
            Assert.That(answer, Is.EqualTo(expected));
        }

        [Test]
        public void Problem11()
        {
            var input = @"08 02 22 97 38 15 00 40 00 75 04 05 07 78 52 12 50 77 91 08
49 49 99 40 17 81 18 57 60 87 17 40 98 43 69 48 04 56 62 00
81 49 31 73 55 79 14 29 93 71 40 67 53 88 30 03 49 13 36 65
52 70 95 23 04 60 11 42 69 24 68 56 01 32 56 71 37 02 36 91
22 31 16 71 51 67 63 89 41 92 36 54 22 40 40 28 66 33 13 80
24 47 32 60 99 03 45 02 44 75 33 53 78 36 84 20 35 17 12 50
32 98 81 28 64 23 67 10 26 38 40 67 59 54 70 66 18 38 64 70
67 26 20 68 02 62 12 20 95 63 94 39 63 08 40 91 66 49 94 21
24 55 58 05 66 73 99 26 97 17 78 78 96 83 14 88 34 89 63 72
21 36 23 09 75 00 76 44 20 45 35 14 00 61 33 97 34 31 33 95
78 17 53 28 22 75 31 67 15 94 03 80 04 62 16 14 09 53 56 92
16 39 05 42 96 35 31 47 55 58 88 24 00 17 54 24 36 29 85 57
86 56 00 48 35 71 89 07 05 44 44 37 44 60 21 58 51 54 17 58
19 80 81 68 05 94 47 69 28 73 92 13 86 52 17 77 04 89 55 40
04 52 08 83 97 35 99 16 07 97 57 32 16 26 26 79 33 27 98 66
88 36 68 87 57 62 20 72 03 46 33 67 46 55 12 32 63 93 53 69
04 42 16 73 38 25 39 11 24 94 72 18 08 46 29 32 40 62 76 36
20 69 36 41 72 30 23 88 34 62 99 69 82 67 59 85 74 04 36 16
20 73 35 29 78 31 90 01 74 31 49 71 48 86 81 16 23 57 05 54
01 70 54 71 83 51 54 69 16 92 33 48 61 43 52 01 89 19 67 48";

            var values = input
                .Replace("\r", " ")
                .Replace("\n", " ")
                .Split(new [] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Length == 2 && s.StartsWith("0")
                    ? s.Substring(1, s.Length - 1)
                    : s)
                .Select(s => int.TryParse(s, out var d) ? d : -1)
                .Where(i => i != -1)
                .ToArray();

            // Not particularly efficient but works 
            var n = 20;
            var answer = 0;
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    var start = i * n + j;
                    var h = CalculateProduct(Enumerable.Range(0, 4).Select(x => start + x).ToArray(), "h");
                    var v = CalculateProduct(Enumerable.Range(0, 4).Select(x => start + x * n).ToArray(), "v");
                    var d1 = CalculateProduct(Enumerable.Range(0, 4).Select(x => start + x * n + x).ToArray(), "d");
                    var d2 = CalculateProduct(Enumerable.Range(0, 4).Select(x => start + x * n - x).ToArray(), "d");
                    answer = new [] {h, v, d1, d2, answer}.Max();
                }
            }

            Assert.That(answer, Is.EqualTo(70600674));
            
            int CalculateProduct(int[] indices, string type)
            {
                bool valid;
                switch (type)
                {
                    case "h":
                        valid = indices.Select(i => i / n).Distinct().Count() == 1;
                        break; 
                    case "v":
                        valid = indices.All(i => i < n * n);
                        break; 
                    case "d":
                        valid = indices.Select(i => i / n).Distinct().Count() == 4 && indices.All(i => i < n * n);
                        break; 
                    default:
                        valid = false;
                        break;
                }

                return valid
                    ? indices.Select(i => values[i]).Aggregate(1, (x, y) => x * y)
                    : 0;
            }
        }
    }
}