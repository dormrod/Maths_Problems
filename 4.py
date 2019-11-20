import numpy as np
import unittest

"""
Find the largest palindrome made from the product of two N-digit numbers.
"""

def check_palindrome(n):
    """
    Check if a number is palindromic.
    """

    # Convert to string
    n_string = str(n)
    digits = len(n_string)

    # Separate into strings about reflective point
    if digits%2==0:
        # Even case
        digits_2 = digits//2
        left = n_string[:digits_2][::-1]
        right = n_string[digits_2:]
    else:
        # Odd case
        digits_2 = (digits-1)//2
        left = n_string[:digits_2][::-1]
        right = n_string[digits_2+1:]

    # If strings match then palindromic
    return left==right


def largest_palindrome(digits):
    """
    Get largest palindrome from multiple of numbers with given number of digits.
    """

    palindromes = []
    numbers = np.arange(10**digits,10**(digits-1),-1)-1
    for n in numbers:
        for m in numbers:
            nm = n*m
            if check_palindrome(nm):
                palindrome = nm
                palindromes.append(palindrome)
    print(palindromes)
    return np.max(palindromes)


class TestProblem4(unittest.TestCase):
    """
    Test solution.
    """

    def test_check_palindrome(self):
        self.assertEqual(check_palindrome(10),False)
        self.assertEqual(check_palindrome(11),True)
        self.assertEqual(check_palindrome(100),False)
        self.assertEqual(check_palindrome(101),True)

    def test_largest_palindrome(self):
        self.assertEqual(largest_palindrome(2),9009)
        answer = largest_palindrome(3)
        print('Answer: {}'.format(answer))
        self.assertEqual(answer,9009)



if __name__ == "__main__":

    unittest.main()

