import numpy as np
import unittest

"""
Find first digits of sum of large numbers.
"""

numbers = np.genfromtxt('problem_13.dat',dtype=str)


def sum_large_numbers(number_strings,digits=50):
    """
    Find sum of large numbers.
    """

    # Convert strings into base 10 numbers
    numbers = np.zeros((number_strings.size,digits),dtype=int)
    for i,n in enumerate(number_strings):
        numbers[i,:] = [int(x) for x in n[::-1]]

    # Add numbers
    addition = np.zeros((numbers.shape[1]+numbers.shape[0]//10+1),dtype=int)
    for i in range(digits):
        addition[i] = numbers[:,i].sum()

    # Convert to base 10
    sorted = False
    while not sorted:
        for i in range(digits):
            n = [int(x) for x in str(addition[i])[::-1]]
            addition[i] = n[0]
            for j in range(1,len(n)):
                addition[i+j] += n[j]
        if np.all(addition<10):
            sorted = True

    # Generate final number ignoring trailing zeros
    solution = []
    trailing = True
    for n in addition[::-1]:
        if n>0:
            trailing = False
        if not trailing:
            solution.append(n)

    return solution


class TestProblem13(unittest.TestCase):
    """
    Test solution.
    """

    def test_sum_large_numbers(self):
        answer = sum_large_numbers(numbers)[:10]
        print('Answer: {}'.format(answer))
        self.assertEqual(tuple(answer),(5, 5, 3, 7, 3, 7, 6, 2, 3, 0))


if __name__ == "__main__":

    unittest.main()