import numpy as np
import unittest

"""
Find scores of list of names.
"""


names = []
with open('problem_22.dat','r') as f:
    for line in f:
        for n in line.split(','):
            names.append((n.strip('"')))


def name_scores(names):
    """
    Niche problem so just compute score as sum of alphabetical position of letters,
    multiplied by position in list.
    """

    # Sort alphabetically
    names = sorted(names)

    # Get scores
    letter_score = {'A':1, 'B':2, 'C':3, 'D':4, 'E':5, 'F':6, 'G':7, 'H':8, 'I':9, 'J':10, 'K':11, 'L':12, 'M':13,
                    'N':14, 'O':15, 'P':16, 'Q':17, 'R':18, 'S':19, 'T':20, 'U':21, 'V':22, 'W':23, 'X':24, 'Y':25, 'Z':26}
    total_score = 0
    for pos_score, name in enumerate(names):
        name_score = 0
        for l in name:
            name_score += letter_score[l]
        total_score += (pos_score+1)*name_score

    return total_score


class TestProblem22(unittest.TestCase):
    """
    Test solution.
    """

    def test_name_scores(self):
        answer = name_scores(names)
        print('Answer: {}'.format(answer))
        self.assertEqual(answer,871198282)


if __name__ == "__main__":

    unittest.main()