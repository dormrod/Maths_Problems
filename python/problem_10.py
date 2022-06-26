import numpy as np
import unittest
from problem_3 import find_primes

"""
Find sum of all primes below N.
"""


def sum_primes(n):
    """
    Find sum of all primes below n.
    """

    # Get primes from problem 3
    primes = find_primes(n)

    return np.sum(primes)


class TestProblem10(unittest.TestCase):
    """
    Test solution.
    """

    def test_sum_primes(self):
        answer = sum_primes(int(2e6))
        print('Answer: {}'.format(answer))
        self.assertEqual(sum_primes(10),17)
        self.assertEqual(answer,142913828922)


if __name__ == "__main__":

    unittest.main()
