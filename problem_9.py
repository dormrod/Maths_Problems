import numpy as np
import unittest

"""
Find Pythagorean triplets which sum to a given number.
"""

def pythagorean_triplets(n):
    """    
    Find a^2+b^2=c^2 for a<b<c, where a+b+c=n.
    """

    # 1<=a<=n/3, a+1<=b<=n/2, b+1<=c<=n
    triples = []
    for a in np.arange(1,int(n/3)):
        for b in np.arange(a+1,int(n/2)):
            c = n - a - b
            if c>b:
                if a*a+b*b==c*c:
                    triples.append((a,b,c))

    return triples


class TestProblem9(unittest.TestCase):
    """
    Test solution.
    """

    def test_triplets(self):
        answer = np.product(pythagorean_triplets(1000)[0])
        print('Answer: {}'.format(answer))
        self.assertEqual(pythagorean_triplets(12)[0],(3,4,5))
        self.assertEqual(pythagorean_triplets(1000)[0],(200, 375, 425))


if __name__ == "__main__":

    unittest.main()