//Digit of Champernowne's constant and corresponding value (0-9) and associated number
type Champ = {
    digit : int
    value : int
    number : int
}

//Helper type with total number of digits for given number
type Sequence = {
    totalDigits : int
    power : int
    number : int
}


//Calculate integer powers of 10 
let rec IntegerPowerOf10 n =
    match n with
    | 0 -> 1
    | _ -> 10 * IntegerPowerOf10 (n-1)

let NineTimesIntegerPower10 n =
    9 * IntegerPowerOf10 n


//Find number associated with Champernowne digit 

let InitialiseChamp d = 
    { digit = d; value = 0; number = 0 }

let IncrementSequenceDigits s = 
    let number = NineTimesIntegerPower10 s.power
    let addDigits = (s.power+1) * number
    { totalDigits = s.totalDigits + addDigits ; power = s.power + 1; number = s.number + number}

let DecrementSequenceDigits s =
    let number = NineTimesIntegerPower10 (s.power-1)
    let subtractDigits = s.power*number
    { totalDigits = s.totalDigits - subtractDigits ; power = s.power - 1; number = s.number - number}

let rec FindSequencePower c s = 
    if c.digit<=s.totalDigits then DecrementSequenceDigits s else FindSequencePower c (IncrementSequenceDigits s)
    
let FindSequenceNumber c s = 
    let digitDifference = c.digit - s.totalDigits
    let numberQuotient = digitDifference / (s.power+1)
    let numberRemainder = digitDifference % (s.power+1)
    match numberRemainder with
    | 0 -> { totalDigits = c.digit; power = s.power; number = s.number + numberQuotient }
    | _ -> { totalDigits = s.totalDigits + ((numberQuotient+1)*(s.power+1)); power = s.power; number = s.number + numberQuotient + 1}


//Get appropriate digit of number for specific Champernowne digit

//Extract mth digit of an integer n with d digits
let rec GetMthDigitOfInteger m n d =
    let divisor = IntegerPowerOf10 d
    let digit = n / divisor
    let subtraction = digit * divisor
    if d=m then digit else GetMthDigitOfInteger m (n-subtraction) (d-1)
    
let DetermineChampValue c s =
    let digitDifference = s.totalDigits - c.digit
    let value = GetMthDigitOfInteger digitDifference s.number s.power
    { digit = c.digit; value = value; number = s.number}


//Compose functions to calculate value of given Champernowne digit
let CalculateChamp d = 
    let initialChamp = InitialiseChamp d
    let initialSeq = FindSequencePower initialChamp {totalDigits = 0; power = 0; number = 0;}
    let sequence = FindSequenceNumber initialChamp initialSeq
    let champ = DetermineChampValue initialChamp sequence
    champ 


//Solve problem, calculate digits of Champernowne's constant and multiply
let c0 = CalculateChamp 1
let c1 = CalculateChamp 10
let c2 = CalculateChamp 100
let c3 = CalculateChamp 1000
let c4 = CalculateChamp 10000
let c5 = CalculateChamp 100000
let c6 = CalculateChamp 1000000
let answer = c0.value * c1.value * c2.value * c3.value * c4.value * c5.value * c6.value
let assertSolution = answer = 210

printfn "Answer: %d" answer