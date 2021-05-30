module FUtil.Results

let splitResult<'a, 'b> (results: ('a seq * 'b seq)) (result: Result<'a, 'b>) =
    let (success, errors) = results
    match result with
    | Ok value -> (Seq.append success [ value ], errors)
    | Error value -> (success, Seq.append errors [ value ])

/// Split a seq of results by success and failure.
/// Returns a tuple (successful,errors)
let splitResults<'a, 'b> results =
    results
    |> Seq.fold splitResult (Seq.empty :> 'a seq, Seq.empty :> 'b seq)