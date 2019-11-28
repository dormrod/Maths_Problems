import numpy as np
import unittest
from problem_13 import NumBase10

"""
Find sum of digits of large factorial.
"""


def sum_factorial_digits(n):
    """
    Find sum of digits of large factorial.
    """

    # Multiply numbers in base 10
    factorial = NumBase10('1')
    for i in range(2,n+1):
        multiplier = NumBase10(str(i))
        factorial = factorial*multiplier
    result = np.array(factorial.number).sum()

    return result


class TestProblem20(unittest.TestCase):
    """
    Test solution.
    """

    def test_sum_factorial(self):
        self.assertEqual(sum_factorial_digits(3),6)
        answer = sum_factorial_digits(100)
        print('Answer: {}'.format(answer))
        self.assertEqual(answer,648)


if __name__ == "__main__":

    unittest.main()