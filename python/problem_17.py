import numpy as np
import unittest

"""
Convert number to word form.
"""


def number_to_word(number):
    """
    Convert number to word.
    """

    # Set up conversion dictionaries
    convert_0_9 = {0:'',1:'one',2:'two',3:'three',4:'four',5:'five',6:'six',7:'seven',8:'eight',9:'nine'}
    convert_10_19 = {10:'ten',11:'eleven',12:'twelve',13:'thirteen',14:'fourteen',15:'fifteen',16:'sixteen',17:'seventeen',18:'eighteen',19:'nineteen'}
    convert_20_90 = {2:'twenty',3:'thirty',4:'forty',5:'fifty',6:'sixty',7:'seventy',8:'eighty',9:'ninety'}

    # Make word
    num_string = [x for x in str(number)]
    if number<10:
        word = convert_0_9[number]
    elif number<20:
        word = convert_10_19[number]
    elif number<100:
        word = convert_20_90[int(num_string[0])]+" "+convert_0_9[int(num_string[1])]
    elif number<1000:
        word = convert_0_9[int(num_string[0])] + " hundred"
        sub_num = int(num_string[1]+num_string[2])
        if sub_num>0:
            word += " and " + number_to_word(sub_num)
    elif number==1000:
        word = "one thousand"
    else:
        word = None

    return word


def length_number_words(n):
    """
    Find length of all number words between 1 and n inclusive.
    """

    length = 0
    for i in range(1,n+1):
        word = number_to_word(i).replace(" ","")
        length += len(word)

    return length


class TestProblem17(unittest.TestCase):
    """
    Test solution.
    """

    def test_number_word_len(self):
        answer = length_number_words(1000)
        print('Answer: {}'.format(answer))
        self.assertEqual(answer,21124)


if __name__ == "__main__":

    unittest.main()