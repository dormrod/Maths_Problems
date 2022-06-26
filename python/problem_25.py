import numpy as np
import unittest
from problem_13 import NumBase10

"""
Find first Fibonacci number with N digits.
"""


def fibonacci_n_digits(n):
    """
    Find first fibonacci number with n digits.
    """

    # Make Fibonacci sequence in base 10 and stop when reach n digits
    n_0 = NumBase10('1')
    n_1 = NumBase10('2')
    i = 3
    while True:
        n_2 = n_0 + n_1
        i += 1
        if n_2.digits==n:
            break
        else:
            n_0 = n_1
            n_1 = n_2

    return i

class TestProblem25(unittest.TestCase):
    """
    Test solution.
    """

    def test_fibonacci(self):
        self.assertEqual(fibonacci_n_digits(2),7)
        answer = fibonacci_n_digits(1000)
        print('Answer: {}'.format(answer))
        # self.assertEqual(answer,31626)
        # print(abundant_sum())

if __name__ == "__main__":

    unittest.main()