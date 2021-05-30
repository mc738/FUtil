namespace FUtil.Serialization

open System
open System.Text

module Utilities =
    let bytesToString (data: byte array) = Encoding.UTF8.GetString data
    
    let stringToBytes (data: string) = Encoding.UTF8.GetBytes data
    
module Json =

    open System.Text.Json
        
    let tryDeserialize<'a> (json: string) =
        
        try
            let r = JsonSerializer.Deserialize<'a>(json)
            Ok r
        with
        | :? ArgumentNullException -> Error "Arguments can not be null"
        | :? JsonException -> Error "Input is not json"
        | :? NotSupportedException -> Error "There is no compatible JsonConverter for returnType or its serializable members."
            
    let trySerialize<'a> (data: 'a) =
        try
            let r = JsonSerializer.Serialize<'a> data
            
            Ok r
        with
        | :? ArgumentNullException -> Error "Arguments can not be null"
        | :? ArgumentException -> Error "InputType is not compatible with value."
        | :? NotSupportedException -> Error "There is no compatible JsonConverter for inputType or its serializable members."