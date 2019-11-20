import numpy as np
import unittest

"""
Find the sum of the multiples of 3 of 5 below 1000.
"""

def sum_multiples_3_5(n=1000):
    """
    Find the sum of the multiples of 3 or 5 below n.
    """

    natural_numbers = np.arange(n)
    mask_mult_3 = natural_numbers%3==0 # Multiples of 3
    mask_mult_5 = natural_numbers%5==0 # Multiples of 5
    mask_mult_3_5 = mask_mult_3 + mask_mult_5 # Multiples of 3 and 5
    mult_3_5 = natural_numbers[mask_mult_3_5]

    return mult_3_5.sum()


class TestProblem1(unittest.TestCase):
    """
    Test solution.
    """

    def test_answer(self):
        self.assertEqual(sum_multiples_3_5(1000), 233168)


if __name__ == "__main__":

    unittest.main()
