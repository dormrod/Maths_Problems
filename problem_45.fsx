let InverseTriangle n:int64 = 
    int64 ((-1.0+sqrt(1.0+8.0*(float n)))/2.0)
    
let InversePentagonal n:int64 = 
    int64 ((1.0+sqrt(1.0+24.0*(float n)))/6.0)

let InverseHexagonal n:int64 = 
    int64 ((1.0+sqrt(1.0+8.0*(float n)))/4.0)
   
let Triangle n:int64 = 
    n*(n+1L)/2L

let Pentagonal n:int64 = 
    n*(3L*n-1L)/2L

let Hexagonal n:int64 = 
    n*(2L*n-1L)

let GetTriSeq lower upper = 
    let l = InverseTriangle lower 
    let u = InverseTriangle upper
    let s = [l..u]
    (List.map Triangle) s

let GetPentSeq lower upper = 
    let l = InversePentagonal lower 
    let u = InversePentagonal upper
    let s = [l..u]
    (List.map Pentagonal) s

let GetHexSeq lower upper = 
    let l = InverseHexagonal lower 
    let u = InverseHexagonal upper
    let s = [l..u]
    (List.map Hexagonal) s

let Merge (a:int64 list) (b:int64 list) (c:int64 list)= 
    let rec MergeInner (aa:int64 list) (bb:int64 list) (c:int64 list) =
        match (aa,bb) with
        | (x::xs,y::ys) -> 
            if x<=y then x::(MergeInner xs bb c)
            else y::(MergeInner aa ys c)
        | (xs,[]) -> xs@c
        | ([],ys) -> ys@c
    let d = if a.[0]<= b.[0] then MergeInner a b [] else MergeInner b a []
    if c.[0]<=d.[0] then MergeInner c d [] else MergeInner d c []

let Common a b c =
    let d = Merge a b c
    let rec CommonInner d = 
        match d with
        | x::(y::z::rest as xs) -> if x=y && y=z then x::(CommonInner xs) else CommonInner xs
        | _ -> [] 
    CommonInner d

let CommonBetween lower upper = 
    let t = GetTriSeq lower upper 
    let p = GetPentSeq lower upper 
    let h = GetHexSeq lower upper 
    Common t p h

let rec FindNext (lower:int64) (upper:int64) (inc:int64) (max:int64) = 
    if upper>=max then printfn "%s" "Max reached"
    else
        let c = CommonBetween lower upper
        if c <>[] then printfn "%A" c
        FindNext (lower+inc) (upper+inc) inc max
    

let answer = FindNext 1L 10000L 10000L 10000000000L
