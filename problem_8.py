import numpy as np
import unittest

"""
Find largest product of adjacent digits in sequence.
"""


with open('problem_8.dat', 'r') as f:
    number = ""
    for line in f:
        number += line


def find_largest_product(seq,n):
    """
    Find largest prodcut of n consecutive numbers in sequence
    """

    # Convert sequence to numpy array of integers
    seq = np.array([int(x) for x in seq.replace("\n","")],dtype=int)

    # Calculate product of each block of length n
    largest_product = 0
    for i in range(seq.size-n):
        mask = np.zeros_like(seq,dtype=bool)
        mask[i:i+n] = 1
        sub_seq = seq[mask]
        product = np.prod(sub_seq)
        if product>largest_product:
            largest_product = product

    return largest_product


class TestProblem8(unittest.TestCase):
    """
    Test solution.
    """

    def test_largest_product(self):
        answer = find_largest_product(number,13)
        print('Answer: {}'.format(answer))
        self.assertEqual(find_largest_product(number,4),5832)
        self.assertEqual(answer,23514624000)


if __name__ == "__main__":

    unittest.main()
