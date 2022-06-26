import numpy as np
import unittest
from problem_13 import NumBase10

"""
Find sum of digits of extremely large number.
"""


def sum_digits_power_2(n):
    """
    Find the sum of the digits of a large power of 2.
    """

    # Use base 10 numbers from problem 13
    num_2 = NumBase10("2")
    power_2 = NumBase10("2")
    for i in range(2,n+1):
        power_2 = power_2*num_2

    return np.sum(power_2.number)


class TestProblem16(unittest.TestCase):
    """
    Test solution.
    """

    def test_sum_digits(self):
        answer = sum_digits_power_2(1000)
        print('Answer: {}'.format(answer))
        self.assertEqual(answer,1366)


if __name__ == "__main__":

    unittest.main()
