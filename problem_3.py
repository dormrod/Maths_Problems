import numpy as np
import unittest

"""
Find the largest prime factor of a large number.
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

    def test_primes(self):
        primes_lt_20 = np.array([2,3,5,7,11,13,17,19])
        primes = find_primes(20)
        self.assertEqual(np.all(primes-primes_lt_20==0),True)

    def test_prime_factors(self):
        prime_factors_11 = np.array([11])
        prime_factors_20 = np.array([2,2,5])
        self.assertEqual(np.all(find_prime_factors(11)-prime_factors_11==0),True)
        self.assertEqual(np.all(find_prime_factors(20)-prime_factors_20==0),True)

    def test_answer(self):
        answer = highest_prime_factor(600851475143)
        print('Answer: {}'.format(answer))
        self.assertEqual(answer, 6857)


if __name__ == "__main__":

    unittest.main()

