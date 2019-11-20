import numpy as np
import unittest

"""
Find the largest prime factor of the number 600851475143.
"""

def find_primes(n):
    """
    Find primes up to given value.
    """

    # Check if prime by dividing by previous primes
    primes = [2]
    for i in range(3,n+1,2):
        prime = True
        max_prime = int(np.sqrt(i)) # Cannot exceed greater than square root of number
        for p in primes:
            if i%p==0:
                prime = False
                break
            elif p>max_prime:
                break
        if prime:
            primes.append(i)

    return np.array(primes)


def find_prime_factors(n):
    """
    Find prime factors of a value.
    """

    # Find primes up to root of n (highest prime factor)
    primes = find_primes(int(np.sqrt(n)))

    # Recur finding prime factors
    prime_factors = recursive_prime_factors(n,primes,[])

    return np.array(prime_factors)


def recursive_prime_factors(n,primes,factors):
    """
    Find prime factors of a number recursively.
    """

    # Find if prime or first prime factor and quotient
    prime = True
    for p in primes[primes<=int(np.sqrt(n))]:
        if n%p==0:
            prime=False
            m=n//p
            factors.append(p)
            break

    # Recur quotient or add prime factor
    if not prime:
        factors = recursive_prime_factors(m,primes,factors)
    else:
        factors.append(n)

    return factors


def highest_prime_factor(n):
    """
    Get largest prime factor of number.
    """

    prime_factors = find_prime_factors(n)

    return np.max(prime_factors)


class TestProblem3(unittest.TestCase):
    """
    Test solution.
    """

    def test_answer(self):
        self.assertEqual(highest_prime_factor(600851475143), 6857)


if __name__ == "__main__":

    unittest.main()

