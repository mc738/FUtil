module FUtil.Pipelines

let bind switchFunction twoTrackInput =
    match twoTrackInput with
    | Ok v -> switchFunction v
    | Error e -> Error e

let (>==) twoTrackInput switchFunction = bind switchFunction twoTrackInput

let (>=>) switch1 switch2 x =
    match switch1 x with
    | Ok v -> switch2 v
    | Error e -> Error e

let switch f x = f x |> Ok

let map oneTrackFunction twoTrackInput =
    match twoTrackInput with
    | Ok v -> Ok(oneTrackFunction v)
    | Error e -> Error e

let tee f x =
    f x |> ignore
    x

let tryCatch f x =
    try
        f x |> Ok
    with ex -> Error ex.Message

let doubleMap successFunction failureFunction twoTrackInput =
    match twoTrackInput with
    | Ok v -> Ok(successFunction v)
    | Error e -> Error(failureFunction e)

let succeed x = Ok x

let fail x = Error x


let plus addSuccess addError switch1 switch2 x =
    match (switch1 x) (switch2 x) with
    | Ok v1, Ok v2 -> Ok(addSuccess v1 v2)
    | Error e, Ok _ -> Error e
    | Ok _, Error e -> Error e
    | Error e1, Error e2 -> Error(addError e1 e2)

