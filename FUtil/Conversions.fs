module FUtil.Conversions

open System
open System.Security.Cryptography

let bytesToHex data =
    data                                                                
    |> Array.map (fun (x:byte) -> System.String.Format("{0:X2}", x))
    |> String.concat String.Empty

let bytesToBase64 data = Convert.ToBase64String data

let hashPassword (algo: HashAlgorithmName) (iterations: int) (hashSize: int) (salt: byte array) (password: byte array) =
        
   use h = new Rfc2898DeriveBytes(password, salt, iterations, algo)
   
   h.GetBytes(hashSize)