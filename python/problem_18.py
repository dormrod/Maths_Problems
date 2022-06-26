import numpy as np
import unittest

"""
Find maximum path sum through triangle.
"""


triangle = []
with open('problem_18.dat','r') as f:
    for line in f:
        triangle.append([int(x) for x in line.split()])


def brute_force_maximum_path_sum(path,triangle,max_sum):
    """
    Find maximum path sum by brute force.
    """

    # Find paths recursively, then sum
    level = len(path)
    if level==0:
        path.append(0)
        path_sum = brute_force_maximum_path_sum(path,triangle,max_sum)
        return path_sum
    elif level<len(triangle):
        i = path[-1]
        j = i+1
        path_1 = [x for x in path]
        path_1.append(i)
        path_sum_1 = brute_force_maximum_path_sum(path_1,triangle,max_sum)
        if j<=level:
            path_2 = [x for x in path]
            path_2.append(j)
            path_sum_2 = brute_force_maximum_path_sum(path_2,triangle,max_sum)
        else:
            path_sum_2 = 0
        path_sum = np.max([path_sum_1,path_sum_2])
        if path_sum>max_sum:
            max_sum = path_sum
        return max_sum
    else:
        path_sum = 0
        for i,j in enumerate(path):
            path_sum += triangle[i][j]
        return path_sum


class TestProblem18(unittest.TestCase):
    """
    Test solution.
    """

    def test_brute_force(self):
        answer = brute_force_maximum_path_sum([],triangle,0)
        print('Answer: {}'.format(answer))
        self.assertEqual(answer,1074)


if __name__ == "__main__":

    unittest.main()