import numpy as np
import unittest

"""
Find first digits of sum of large numbers.
"""

numbers = np.genfromtxt('problem_13.dat',dtype=str)


class NumBase10:
    """
    Large number stored in base 10.
    """

    def __init__(self,number=None,digits=0):
        """
        Initialise with number of digits or convert string to number.
        """

        # Store number in powers of 10
        if number is None:
            self.digits = digits
            self.number = np.zeros(self.digits,dtype=int)
        else:
            self.digits = len(number)
            self.number = np.zeros(self.digits,dtype=int)
            for i,n in enumerate(number[::-1]):
                self.number[i] = n


    def __add__(self,other):
        """
        Add another number in base 10.
        """

        result = NumBase10(digits=np.max([self.digits,other.digits])+1)
        for i,n in enumerate(self.number):
            result.number[i] += n
        for i,n in enumerate(other.number):
            result.number[i] += n

        result.recalculate()

        return result


    def __mul__(self, other):
        """
        Multiply another number in base 10.
        """

        result = NumBase10(digits=np.max([self.digits,other.digits])*2)
        for i,n in enumerate(self.number):
            for j,m in enumerate(other.number):
                result.number[i+j] += n*m

        result.recalculate()

        return result


    def recalculate(self):
        """
        Recalculate into base 10.
        """

        # Make sure all elements less than 10
        sorted = False
        while not sorted:
            for i in range(self.digits):
                n = [int(x) for x in str(self.number[i])[::-1]]
                self.number[i] = n[0]
                for j in range(1,len(n)):
                    self.number[i+j] += n[j]
            if np.all(self.number<10):
                sorted = True

        # Remove trailing zeros
        trailing_count = 0
        for n in self.number[::-1]:
            if n>0:
                break
            else:
                trailing_count += 1
        self.digits -= trailing_count
        number = np.zeros((self.digits),dtype=int)
        number[:] = self.number[:self.digits]
        self.number = number


def sum_large_numbers(number_strings):
    """
    Find sum of large numbers.
    """

    # Convert to base 10 and sum
    n1 = NumBase10(number_strings[0])
    for n in number_strings[1:]:
        n2 = NumBase10(n)
        n1 = n1+n2

    return n1.number[::-1]


class TestProblem13(unittest.TestCase):
    """
    Test solution.
    """

    def test_base10_class(self):
        number1 = NumBase10(number="123")
        number2 = NumBase10(number="879")
        number3 = number1+number2
        self.assertEqual(tuple(number1.number),(3,2,1))
        self.assertEqual(tuple(number3.number),(2,0,0,1))
        number4 = NumBase10(number="12")
        number5 = NumBase10(number="321")
        number6 = number4*number5
        self.assertEqual(tuple(number6.number),(2,5,8,3))

    def test_sum_large_numbers(self):
        answer = sum_large_numbers(numbers)[:10]
        print('Answer: {}'.format(answer))
        self.assertEqual(tuple(answer),(5, 5, 3, 7, 3, 7, 6, 2, 3, 0))


if __name__ == "__main__":

    unittest.main()