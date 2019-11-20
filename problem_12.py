import numpy as np
import unittest
from problem_3 import find_primes, recursive_prime_factors

"""
Find highly divisible triangle numbers.
"""


def find_divisors(n):
    """
    Find all divisors of a number.
    Very slow - not a feasible way of doing this.
    """

    # Divisors can be up to half the number, and then number itself
    possible_divisors = np.arange(1,int(n/2)+2)
    possible_divisors[-1] = n
    divisor_mask = np.ones_like(possible_divisors,dtype=bool)

    # Work from lowest to highest possible divisors and check division
    for i,d in enumerate(possible_divisors):
        if divisor_mask[i]:
            # If not a divisor rule out this number and all higher multiples
            if n%d!=0:
                divisor_mask[i] = False
                divisor_mask[possible_divisors%d==0] = False

    # Get divisors
    divisors = possible_divisors[divisor_mask]

    return divisors


def find_divisors_from_factorisation(n,primes):
    """
    Get all divisors from prime factorisation.
    """

    # Get prime factors
    factors = recursive_prime_factors(n,primes,[])

    # Get unique factors and powers
    factors, multiplicity = np.unique(factors,return_counts=True)

    # Find divisors
    divisors = [1]
    for i in range(factors.size):
        f = factors[i]
        m = multiplicity[i]
        divisors += [d*f**k for k in range(1,m+1) for d in divisors]

    return np.array(divisors)


def first_divisible_triangle_number(n):
    """
    Find first triangle number with at least n divisors.
    :param n: 
    :return: 
    """

    # Cache prime numbers
    max_prime = 1000
    max_prime_sq = max_prime*max_prime
    primes = find_primes(max_prime)

    triangle = 1
    i = 2
    while True:
        divisors = find_divisors_from_factorisation(triangle,primes)
        if divisors.size>=n:
            break
        triangle += i
        i += 1
        # Update prime cache if necessary (triangle numbers cannot be prime)
        if triangle>max_prime_sq:
            max_prime *= 10
            max_prime_sq = max_prime*max_prime
            primes = find_primes(max_prime)

    return triangle


class TestProblem12(unittest.TestCase):
    """
    Test solution.
    """

    def test_divisors(self):
        self.assertEqual(tuple(find_divisors(24)),(1,2,3,4,6,8,12,24))
        self.assertEqual(tuple(find_divisors(17)),(1,17))

    def test_divisors_from_factors(self):
        primes = find_primes(100)
        self.assertEqual(tuple(np.sort(find_divisors_from_factorisation(24,primes))),(1,2,3,4,6,8,12,24))
        self.assertEqual(tuple(np.sort(find_divisors_from_factorisation(17,primes))),(1,17))

    def test_divisible_triangle_nums(self):
        answer = first_divisible_triangle_number(501)
        print('Answer: {}'.format(answer))
        self.assertEqual(answer,76576500)


if __name__ == "__main__":

    unittest.main()
