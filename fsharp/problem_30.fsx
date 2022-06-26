let rec SumDigitsPower5 n k = 
    let remainder = n%k
    let nn = n - remainder
    let digit = 10*remainder/k
    let digit5 = digit*digit*digit*digit*digit
    match nn with
    | 0 -> digit5
    | _ -> digit5 + SumDigitsPower5 nn (k*10)

let rec SumOfPower5Numbers n_start n_end = 
    let add = if n_start = SumDigitsPower5 n_start 10 then n_start else 0
    if n_start = n_end then 0 else add + SumOfPower5Numbers (n_start-1) n_end

let a100k = SumOfPower5Numbers 100000 2 
let a200k = SumOfPower5Numbers 200000 100001
let a300k = SumOfPower5Numbers 355000 200002
let answer = a100k + a200k + a300k

let assertSolution = answer = 443839

printfn "Answer: %d" answer
