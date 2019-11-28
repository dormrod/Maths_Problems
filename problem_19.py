import numpy as np
import unittest

"""
How many Sundays fell on the first of the month during the 20th century?
"""


def dates():
    """
    Niche problem so just solve.
    """

    # Fast forward to the end of 1900
    _,curr_day = first_of_month(1900,6)

    # Check years
    first_day_counts = np.zeros(7,dtype=int)
    for year in range(1901,2001):
        first_days, curr_day = first_of_month(year,curr_day)
        for d in first_days:
            first_day_counts[d] += 1

    return first_day_counts[0]


def first_of_month(year,prev_day):
    """
    Days of the first of the month of given year, using previous day
    """

    # Get days in each month
    leap = 0
    if year%100:
        if year%400:
            leap = 1
    elif year%4:
        leap = 1
    days_in_month = [31,28+leap,31,30,31,30,31,31,30,31,30,31]

    # Get day at start of each month
    first_days = np.zeros(12,dtype=int)
    curr_day = (prev_day + 1)%7
    first_days[0] = curr_day
    for i in range(11):
        curr_day += days_in_month[i]
        first_days[i+1] = curr_day%7

    # Get last day of year
    curr_day += days_in_month[-1] - 1
    last_day = curr_day%7

    return first_days, last_day


class TestProblem19(unittest.TestCase):
    """
    Test solution.
    """

    def test_dates(self):
        answer = dates()
        print('Answer: {}'.format(answer))
        self.assertEqual(answer,171)


if __name__ == "__main__":

    unittest.main()