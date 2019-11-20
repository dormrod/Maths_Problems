import numpy as np
import unittest

"""
Find the difference between the sum of squares and the square of the sum of N natural numbers.
"""

def difference(n):
    """
    Difference between sum of squares and square of sum.
    """

    natural_numbers = np.arange(1,n+1)

    return np.abs(np.sum(natural_numbers**2)-np.sum(natural_numbers)**2)


class TestProblem6(unittest.TestCase):
    """
    Test solution.
    """

    def test_difference(self):
        answer = difference(100)
        print('Answer: {}'.format(answer))
        self.assertEqual(difference(10),2640)
        self.assertEqual(answer,25164150)


if __name__ == "__main__":

    unittest.main()