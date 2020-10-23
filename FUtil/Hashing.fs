module FUtil.Hashing

open System.Security.Cryptography

let private computeHash name (data:byte array) =
    HashAlgorithm.Create(name).ComputeHash data

/// Get the `MD5` hash of a byte array, as a bytes.      
let md5 data = computeHash "MD5" data

/// Get the `MD5` hash of a byte array, as a hex string.
let md5Hex data = ConversionHelpers.bytesToHex (md5 data)    

/// Get the `md5` hash of a byte array, as a base64 string.        
let sh1Base64 data = ConversionHelpers.bytesToBase64 (md5 data)

/// Get the `SHA1` hash of a byte array, as a bytes.    
let sha1 data = computeHash "SHA1" data

/// Get the `SHA1` hash of a byte array, as a hex string.
let sha1Hex data = ConversionHelpers.bytesToHex (sha1 data)    

/// Get the `SHA1` hash of a byte array, as a base64 string.        
let sha1Base64 data = ConversionHelpers.bytesToBase64 (sha1 data)

/// Get the `SHA256` hash of a byte array, as a bytes.
let sha256 data = computeHash "SHA256" data

/// Get the `SHA256` hash of a byte array, as a hex string.
let sha256Hex data = ConversionHelpers.bytesToHex (sha256 data)    

/// Get the `SHA256` hash of a byte array, as a base64 string.        
let sh256Base64 data = ConversionHelpers.bytesToBase64 (sha256 data)

/// Get the `SHA384` hash of a byte array, as a bytes.
let sha384 data = computeHash "SHA384" data

/// Get the `SHA384` hash of a byte array, as a hex string.
let sha384Hex data = ConversionHelpers.bytesToHex (sha384 data)    

/// Get the `SHA384` hash of a byte array, as a base64 string.        
let sh384Base64 data = ConversionHelpers.bytesToBase64 (sha384 data)

/// Get the `SHA512` hash of a byte array, as a bytes.
let sha512 data = computeHash "SHA512" data

/// Get the `SHA512` hash of a byte array, as a hex string.
let sha512Hex data = ConversionHelpers.bytesToHex (sha512 data)    

/// Get the `SHA512` hash of a byte array, as a base64 string.        
let sh512Base64 data = ConversionHelpers.bytesToBase64 (sha512 data)