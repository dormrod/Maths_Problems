import numpy as np
import unittest

"""
Find number of lattice paths through a grid of given dimensions.
"""


def recursive_lattice_paths(x,y):
    """
    Recursively find lattice paths through grid, moving only right and down.
    Too slow to do on 20x20 directly.
    """

    # Recur
    if x>0 and y>0:
        return recursive_lattice_paths(x-1,y) + recursive_lattice_paths(x,y-1)
    elif x==0 and y>0:
        # Reached right
        return 1
    elif x>0 and y==0:
        # Reached bottom
        return 1
    else:
        # Reached bottom right
        return 0


def lattice_paths(n):
    """
    Find all lattice paths through grid of dimensions nxn.
    """

    # Cache number of lattice paths
    paths = np.zeros((n+1,n+1),dtype=int)
    paths[0,1] = 1
    paths[1,0] = 1
    paths[1,1] = 2

    # Find lattice paths for axb lattices
    for i in range(2,n+1):
        paths[i,0] = paths[i-1,0]
        paths[0,i] = paths[0,i-1]
        for j in range(1,i+1):
            paths[i,j] = paths[i,j-1] + paths[i-1,j]
            paths[j,i] = paths[i,j]

    return paths[n,n]


class TestProblem15(unittest.TestCase):
    """
    Test solution.
    """

    def test_recursive_lattice_paths(self):
        self.assertEqual(recursive_lattice_paths(2,2),6)

    def test_lattice_paths(self):
        answer = lattice_paths(20)
        print('Answer: {}'.format(answer))
        self.assertEqual(lattice_paths(2),6)
        self.assertEqual(answer,137846528820)


if __name__ == "__main__":

    unittest.main()