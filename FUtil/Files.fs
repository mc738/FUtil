module FUtil.Files

open System
open System.IO
open System.Security

let private fileExists path =
    match File.Exists path with
    | true -> Ok ()
    | false -> Error (sprintf "File `%s` does not exist." path)

let private wrapper handler path =
    
    match fileExists path with
    | Ok _ ->
        try 
            Ok (handler path)
        with
        | :? ArgumentException -> Error "path is a zero-length string, contains only white space, or contains one or more invalid characters defined by the GetInvalidPathChars() method."
        | :? ArgumentNullException -> Error "path is null." // NOTE This should not throw.
        | :? DirectoryNotFoundException -> Error "path is invalid (for example, it is on an unmapped drive)."
        | :? FileNotFoundException -> Error "The file specified by path was not found." // NOTE This should not throw.
        | :? IOException -> Error "An I/O error occurred while opening the file."
        | :? PathTooLongException -> Error "path exceeds the system-defined maximum length." // NOTE This should not throw.
        | :? SecurityException -> Error "The caller does not have the required permission."
        | :? UnauthorizedAccessException -> Error "path specifies a file that is read-only."
    | Error e -> Error e
    
let tryReadLines path = wrapper (fun path -> File.ReadLines path) path
    
let tryReadBytes path = wrapper (fun path -> File.ReadAllBytes path) path
    
let tryReadText path = wrapper (fun path -> File.ReadAllText path) path
    
let tryWriteLines path lines = wrapper (fun path -> File.WriteAllLines(path, lines)) path

let tryWriteBytes path bytes = wrapper (fun path -> File.WriteAllBytes(path, bytes)) path

let tryWriteText path text = wrapper (fun path -> File.WriteAllText(path, text)) path