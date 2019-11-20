import numpy as np
import unittest

"""
Find the sum of the even Fibonacci numbers below N.
"""

def fibonacci(cutoff):
    """
    Find all Fibonacci numbers below cutoff.
    """

    # Hardcode first two numbers in sequence
    if cutoff<1:
        return []
    elif cutoff<2:
        return [1]
    elif cutoff<3:
        return [1,2]

    # Add previous two numbers in sequence until cutoff reached
    seq = [1,2]
    k_0 = 1
    k_1 = 2
    while True:
        k_2 = k_1 + k_0
        if k_2>cutoff:
            break
        seq.append(k_2)
        k_0 = k_1
        k_1 = k_2

    return np.array(seq)


def sum_even_fibonacci(cutoff):
    """
    Sum of all even fibonacci below cutoff.
    """

    # Get all Fibonacci
    fib = fibonacci(cutoff)

    # Get even members and sum
    mask_even = fib%2==0
    even_fib = fib[mask_even]

    return even_fib.sum()


class TestProblem2(unittest.TestCase):
    """
    Test solution.
    """

    def test_fibonacci(self):
        seq = fibonacci(1000)
        fib = np.all(seq[:-2]+seq[1:-1]==seq[2:])
        self.assertEqual(fib,True)

    def test_answer(self):
        answer = sum_even_fibonacci(4e6)
        print('Answer: {}'.format(answer))
        self.assertEqual(answer, 4613732)


if __name__ == "__main__":

    unittest.main()
