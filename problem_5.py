import numpy as np
import unittest

"""
Find the smallest positive number that is evenly divisible by all of the numbers from 1 to N.
"""

def smallest_evenly_divisible(n):
    """
    Find smallest evenly divisible number for given range.
    """

    m = n
    divisors = np.arange(n-1,1,-1)
    while True:
        even = True
        for d in divisors:
            if m%d != 0:
                even = False
                break
        if even:
            break
        m += n

    return m


class TestProblem5(unittest.TestCase):
    """
    Test solution.
    """

    def test_divisible(self):
        self.assertEqual(smallest_evenly_divisible(10),2520)
        answer = smallest_evenly_divisible(20)
        print('Answer: {}'.format(answer))
        self.assertEqual(answer,9009)


if __name__ == "__main__":

    unittest.main()