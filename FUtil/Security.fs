module FUtil.Security

open System
open System.IO
open System.Security.Cryptography

type EncryptionContext = {
    // Cspp: CspParameters
    // Rsa: RSACryptoServiceProvider
    Key: byte array
    IV: byte array
}
    
let encryptBytesAes context (data: byte array) =
    use aes = Aes.Create()
    
    aes.Padding <- PaddingMode.PKCS7
             
    let encryptor = aes.CreateEncryptor(context.Key, context.IV)
   
    use ms = new MemoryStream()
    use cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write)

    cs.Write(ReadOnlySpan(data))
    cs.FlushFinalBlock()
    
    ms.ToArray() 
    
let decryptBytesAes context (cipher : byte array) =
    use aes = Aes.Create()
    
    aes.Padding <- PaddingMode.PKCS7
     
    let decryptor = aes.CreateDecryptor(context.Key, context.IV)
    
    use ms = new MemoryStream(cipher)
    use cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read)
    
    Streams.readAllBytes cs
    
let generateSalt length =
        
        let bytes: byte array = Array.zeroCreate length
        
        use rng = new RNGCryptoServiceProvider()
        
        rng.GetBytes(bytes)
        
        bytes