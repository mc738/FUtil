module FUtil.Arrays

open System

let inBounds<'a> (arr: 'a array) i = i >= 0 && i < arr.Length

let tryGet input i =
    match inBounds input i with
    | true -> Some(input.[i])
    | false -> None
            
/// A helper look ahead function.
/// Example:
/// ```
/// let lookAhead1 = lookAhead 1
/// let item = lookAhead1 [| 1,2,3,4 |] 2
/// ```
let lookAhead x input currentIndex = tryGet input (currentIndex + x)

/// A helper look back function.
/// Example:
/// ```
/// let lookBack1 = lookBack 1
/// let item = lookBack1 [| 1,2,3,4 |] 2
/// ```        
let lookBack x input currentIndex = tryGet input (currentIndex - x)

/// Append an item to the end of an existing array,
/// returning a new array consisting of the old array then the item.
let append item arr = Array.append arr [| item |]

/// Infix array append.
let (|<+) = append 

/// Push an item to the front of a array,
/// returning a new array consisting of the item then the old array.
let push item arr = Array.append [| item |] arr

/// Infix array push
let (+>|) = push

let removeFirst<'a> (arr: 'a array) =
    match arr.Length with
    | l when l < 2 -> Array.empty 
    | _ -> arr.[1..]
    
let removeLast<'a> (arr: 'a array) =
    match arr.Length with
    | l when l <= 1 -> Array.empty 
    | _ -> arr.[0..(arr.Length - 1)]

let removeAt index arr =
    match inBounds arr index with
    | true ->
        match index with
        | _ when index = 0 -> removeFirst arr
        | _ when index = arr.Length - 1 -> arr.[0..(arr.Length - 1)]
        | _ -> Array.append arr.[0..(index - 1)] arr.[(index + 1)..]
    | false -> arr
    
let insertAt index item arr =
    match inBounds arr index with
    | true ->
        match index with
        | _ when index = 0 -> item +>| arr 
        | _ when index = arr.Length - 1 -> item |<+ arr  
        | _ -> Array.concat [ arr.[0..(index - 1)]; [| item |]; arr.[(index)..] ]
    | false -> item |<+ arr

module Chars =
    
    let appendChar arr (char: Char) = append char arr
        
    let (++) arr char = appendChar arr char
    
    let readTo c startIndex (input: char array) =
            let rec handler (state, i) =
                match i with
                | _ when i < 0 || i >= input.Length -> None //state // Guard.
                | _ when i = input.Length - 1 -> Some(input.[i] |<+ state, i)
                | _ when input.[i] = c -> Some(state, i) // Happy path return.
                | _ -> handler (input.[i] |<+ state, i + 1)
            
            handler (Array.empty, startIndex)
    
    let readToMatched openChar closeChar startIndex (input: char array) =
        let rec handler (state, opened, i) =
            match i with
            | _ when i < 0 || i >= input.Length -> None // Guard.
            | _ when input.[i] = closeChar && opened = 1 -> Some(input.[i] +>| state, i) // Happy path return
            | _ when input.[i] = openChar -> handler(input.[i] +>| state, opened + 1, i + 1)
            | _ when input.[i] = closeChar && opened > 1 -> handler(input.[i] +>| state, opened - 1, i + 1)
            | _ -> handler (input.[i] +>| state, opened, i + 1)
            
        handler (Array.empty, 0, startIndex)
    