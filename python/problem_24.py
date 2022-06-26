import numpy as np
from scipy.special import factorial
import unittest

"""
Find nth lexicographic permutation of number range.
"""

def lexicographic_permutation(num_range,n):
    """
    Find nth lexicographic permutation of number range.
    """

    # Sort
    num_range = np.sort(num_range)
    m = num_range.size

    # Get number of permutations for each index
    permutations = np.array([factorial(x) for x in np.arange(m-1,-1,-1)])

    # Find indices for nth permutation
    permutation_indices = []
    position = 0
    p = 1
    while True:
        for i in range(m):
            p += permutations[position]
            if p>n:
                break
        p -= permutations[position]
        permutation_indices.append(i)
        position += 1
        if position>=m:
            break

    # Find permutation
    permutation = ""
    mask = np.ones_like(num_range,dtype=bool)
    for i in permutation_indices:
        val = num_range[mask][i]
        permutation += str(val)
        mask[val] = False
    print(permutation_indices)
    print(permutation)

    return permutation


class TestProblem24(unittest.TestCase):
    """
    Test solution.
    """

    def test_lexicographic(self):
        self.assertEqual(lexicographic_permutation([0,1,2],1),"012")
        self.assertEqual(lexicographic_permutation([0,1,2],2),"021")
        self.assertEqual(lexicographic_permutation([0,1,2],3),"102")
        self.assertEqual(lexicographic_permutation([0,1,2],6),"210")
        answer = lexicographic_permutation(np.arange(10),1000000)
        print('Answer: {}'.format(answer))
        # self.assertEqual(answer,171)

if __name__ == "__main__":

    unittest.main()