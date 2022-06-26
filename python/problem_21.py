import numpy as np
import unittest
from problem_3 import find_primes, recursive_prime_factors
from problem_12 import find_divisors_from_factorisation

"""
Find sum of amicable numbers below N.
"""


def find_amicable_numbers(n):
    """
    Find amicable numbers less than n.
    Amicable pair: sum of proper divisors of one number gives the other.
    """

    # Get prime numbers
    primes = find_primes(int(np.sqrt(n)))

    # Find divisors of each number and sum them
    sum_divisors = [0,0]
    for i in range(2,n):
        divs = np.array(find_divisors_from_factorisation(i,primes)[:-1])
        sum_divisors.append(divs.sum())

    # Find amicable pairs
    amicable = []
    for i in range(2,n):
        j = sum_divisors[i]
        if j<n and j!=i:
            k = sum_divisors[j]
            if i==k:
                amicable.append(i)

    return np.array(amicable,dtype=int)


def sum_amicable_numbers(n):
    """
    Sum amicable numbers less than n.
    """

    amicable = find_amicable_numbers(n)

    return amicable.sum()


class TestProblem21(unittest.TestCase):
    """
    Test solution.
    """

    def test_amicable_numbers(self):
        answer = sum_amicable_numbers(10000)
        print('Answer: {}'.format(answer))
        self.assertEqual(answer,31626)


if __name__ == "__main__":

    unittest.main()
