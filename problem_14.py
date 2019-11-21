import numpy as np
import unittest

"""
Find longest Collatz sequence below N.
"""


def next_collatz_number(n):
    """
    Get next Collatz number in sequence.
    n/2 if n even, 3n+1 if n odd.
    """

    if n%2==0:
        return n//2
    else:
        return 3*n+1


def length_collatz(n):
    """
    Get length of all collatz sequences below N.
    """

    # Cache sequence lengths for speed
    collatz = np.ones(n,dtype=int)*-1

    # Check each number up to N to determine sequence length
    for i in range(1,n):
        cl = 1
        m = i
        if i%1000==0: print(i)
        while m!=1:
            if collatz[i] == -1:
                m = next_collatz_number(m)
                cl += 1
            else:
                cl += collatz[i]
                m = 1
        collatz[i] = cl

    return collatz


def max_length_collatz(n):
    """
    Get number below n which has the maximum length collatz sequence.
    """

    collatz = length_collatz(n)

    return np.argmax(collatz)


class TestProblem14(unittest.TestCase):
    """
    Test solution.
    """

    def test_collatz_length(self):
        self.assertEqual(length_collatz(14)[-1],10)

    def test_collatz_max_length(self):
        answer = max_length_collatz(int(1e6))
        print('Answer: {}'.format(answer))
        self.assertEqual(answer,837799)


if __name__ == "__main__":

    unittest.main()