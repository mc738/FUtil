module FUtil.Messaging

open System.Threading.Channels

/// A wrapper class for `System.Threading.Channels.ChannelRead`
type Reader<'a>(reader: ChannelReader<'a>) =

    /// Set the reader listening for items.
    member this.Start handler =
        let rec loop () =
            async {

                // This should get the next item async.
                // Thus not annoying the thread pool
                let! item = reader.ReadAsync() |> Threads.convertValueTask

                // Once an item is received send it to the handler function.
                let cont = handler item

                // Recursive loop.
                if cont then loop () |> ignore
            }

        loop ()
        
    member this.TryGet (d) =
       match reader.TryRead(d) with
       | true -> Some d
       | false -> None
       // let item = reader.TryRead()

/// A wrapper class for `System.Threading.Channels.ChannelWriter`
type Writer<'a>(writer: ChannelWriter<'a>) =

    /// Post a message to the writer.
    member this.Post item =
        match writer.TryWrite item with
        | true -> Ok()
        | false -> Error()

/// Create a mail box processor of type 'a and state of 'b.
let createMBPWithState<'a,'b> (state:'b) handler =
    MailboxProcessor<'a>
        .Start(fun inbox ->
              let rec loop (state) =
                  async {

                      let! item = inbox.Receive()

                      let (cont,newState) = handler item state

                      if cont then return! loop (newState)
                  }

              loop (state))


/// Create a mail box processor of type 'a
/// and assign it a handler for incoming items.
let createMBP<'a> handler =
    MailboxProcessor<'a>
        .Start(fun inbox ->
              let rec loop () =
                  async {

                      let! item = inbox.Receive()

                      let cont = handler item

                      if cont then return! loop ()
                  }

              loop ())


/// Create a channel, and get it's reader and writer.
let createChannel<'a> =
    let channel = Channel.CreateUnbounded<'a>()
    let reader = Reader channel.Reader
    let writer = Writer channel.Writer
    (reader, writer)