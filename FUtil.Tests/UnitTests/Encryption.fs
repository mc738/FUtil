namespace FUtil.Tests.UnitTests

open System.Text
open FUtil
open Microsoft.VisualStudio.TestTools.UnitTesting
open FUtil.Encryption

[<TestClass>]
type Encryption () =

    [<TestMethod>]
    member this.``encrypt decrypt test 1`` () =
        let context = {
            Key = Encoding.UTF8.GetBytes "Super secret key"
            IV = FUtil.Passwords.generateSalt 16 // TODO this not reference `Passwords`.
        }
        
        let expected = "Hello, World!"
        
        let data = Encoding.UTF8.GetBytes expected
        
        let e = Encryption.encryptBytesAes context data
        
        let d = decryptBytesAes context e
        
        let actual = Encoding.UTF8.GetString d
        
        Assert.AreEqual(expected, actual)
        
    [<TestMethod>]
    member this.``encrypt decrypt test 2`` () =
        let context = {
            Key = Encoding.UTF8.GetBytes "Super secret key"
            IV = FUtil.Passwords.generateSalt 16 // TODO this not reference `Passwords`.
        }
        
        let expected = """{"message": "Hello, World!"}"""
        
        let data = Encoding.UTF8.GetBytes expected
        
        let e = Encryption.encryptBytesAes context data
        
        let d = decryptBytesAes context e
        
        let actual = Encoding.UTF8.GetString d
        
        Assert.AreEqual(expected, actual)