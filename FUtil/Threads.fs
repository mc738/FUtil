module FUtil.Threads

open System.Threading.Tasks

/// Send a task to the thread pool
let dispatchTask task =
    // From http://www.fssnip.net/1P/title/Easy-Wrapper-for-thread-pool-work.
    Async.StartAsTask <| async { return task }

/// Convert a ValueTask<'a> to Async<'a>.
let convertValueTask<'a> (task: ValueTask<'a>) = task.AsTask() |> Async.AwaitTask

/// A helper function simulate a synchronously blocking process.
let blockFor time = ()
    // Test - wait one second before replying.
    //Async.Sleep time
    //|> Async.RunSynchronously
    //|> ignore