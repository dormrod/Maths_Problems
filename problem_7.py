import numpy as np
import unittest

"""
Find the Nth prime.
"""

def find_nth_prime(n):
    """
    Find nth prime.
    """

    # Check if prime by dividing by previous primes
    if n==1: return 2
    primes = [2]
    i = 3
    while True:
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
            if len(primes)==n:
                break
        i += 2

    return primes[-1]


class TestProblem7(unittest.TestCase):
    """
    Test solution.
    """

    def test_primes(self):
        answer = find_nth_prime(10001)
        print('Answer: {}'.format(answer))
        self.assertEqual(find_nth_prime(1),2)
        self.assertEqual(find_nth_prime(2),3)
        self.assertEqual(find_nth_prime(6),13)
        self.assertEqual(answer,104743)


if __name__ == "__main__":

    unittest.main()
