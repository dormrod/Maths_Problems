import numpy as np
import unittest
from problem_3 import find_primes, recursive_prime_factors
from problem_12 import find_divisors_from_factorisation

"""
Find sum of all positive integers that cannot be written as the sum of two abundant numbers.
"""


def abundant_numbers(n):
    """
    Find all abundant numbers less than n.
    An abundant number has the sum of its proper divisors greater than the number itself.
    """

    # Get primes
    primes = find_primes(int(np.sqrt(n)))

    # Get divisors and sum proper divisors
    abundant = []
    for i in range(2,n):
        divisors = find_divisors_from_factorisation(i,primes)[:-1]
        if divisors.sum()>i:
            abundant.append(i)

    return np.array(abundant)


def abundant_sum():
    """
    Find all integers that cannot be written as the sum of two abundant numbers.
    Smallest abundant number is 12, and all numbers over 28123 are proven to satisfy this condition.
    """

    # Get abundant numbers
    abundant = abundant_numbers(28123)

    # Find all integers that cannot be written as sum of two abundant by brute force
    exception_mask = np.ones(28124,dtype=bool)
    for i,a in enumerate(abundant):
        for j,b in enumerate(abundant[i:]):
            c = a+b
            if c<28124:
                exception_mask[c] = False
    exceptions = np.arange(28124)[exception_mask]

    return exceptions.sum()


class TestProblem23(unittest.TestCase):
    """
    Test solution.
    """

    def test_abundant_numbers(self):
        abundant = abundant_numbers(100)
        self.assertEqual(abundant[0],12)

    def test_sum_abundant_numbers(self):
        answer = abundant_sum()
        print('Answer: {}'.format(answer))
        self.assertEqual(answer,4179871)


if __name__ == "__main__":

    unittest.main()