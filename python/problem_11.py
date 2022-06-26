import numpy as np
import unittest

"""
Find greatest product in grid.
"""

grid = np.genfromtxt('./problem_11.dat').astype(int)

def grid_greatest_product(grid,n):
    """
    Find greatest product of n adjacent numbers in grid.
    """

    greatest_product = 0
    dim = grid.shape[0]

    # Search horizontal and vertical lines
    mask = np.zeros(dim,dtype=bool)
    for i in range(dim):
        for j in range(dim-n+1):
            mask[:] = 0
            mask[j:j+n] = 1
            product = np.prod(grid[mask,i])
            if product>greatest_product:
                greatest_product = product
            product = np.prod(grid[i,mask])
            if product>greatest_product:
                greatest_product = product

    # Search diagonals
    diagonal = np.zeros(4,dtype=int)
    for i in range(dim-n+1):
        for j in range(dim-n+1):
            for k in range(n):
                diagonal[k] = grid[i+k,j+k]
            product = np.prod(diagonal)
            if product>greatest_product:
                greatest_product = product
    for i in range(n-1,dim):
        for j in range(dim-n+1):
            for k in range(n):
                diagonal[k] = grid[i-k,j+k]
            product = np.prod(diagonal)
            if product>greatest_product:
                greatest_product = product

    return greatest_product


class TestProblem11(unittest.TestCase):
    """
    Test solution.
    """

    def test_grid_product(self):
        answer = grid_greatest_product(grid,4)
        print('Answer: {}'.format(answer))
        # self.assertEqual(sum_primes(10),17)
        # self.assertEqual(answer,142913828922)


if __name__ == "__main__":

    unittest.main()
