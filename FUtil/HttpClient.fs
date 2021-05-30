module FUtil.HttpClient

open System
open System.IO
open System.Net.Http
open System.Threading.Tasks

type ReturnType =
    | String
    | Stream

type ReturnContent =
    | StringContent of string
    | StreamContent of Stream


let initialize url =
    use client = new HttpClient(url)

    client

let tryGet (returnType: ReturnType) (client: HttpClient) (url: string) =
    async {
        try
            let! request = client.GetAsync url |> Async.AwaitTask

            match request.IsSuccessStatusCode with
            | true ->
                match returnType with
                | String ->
                    let! c =
                        request.Content.ReadAsStringAsync()
                        |> Async.AwaitTask

                    return Ok(StringContent c)
                | Stream ->
                    let! s =
                        request.Content.ReadAsStreamAsync()
                        |> Async.AwaitTask

                    return Ok(StreamContent s)
            | false ->
                let! error =
                    request.Content.ReadAsStringAsync()
                    |> Async.AwaitTask

                return Error error
        with
        | :? InvalidOperationException ->
            return Error "The requestUri must be an absolute URI or BaseAddress must be set."
        | :? HttpRequestException ->
            return Error
                       "The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout."
        | :? TaskCanceledException -> return Error "The request failed due to timeout."
    }

let tryPost (returnType: ReturnType) (client: HttpClient) (url: string) (body: byte array) =
    async {
        try

            use content = new ByteArrayContent(body)

            let! request = client.PostAsync(url, content) |> Async.AwaitTask

            match request.IsSuccessStatusCode with
            | true ->
                match returnType with
                | String ->
                    let! c =
                        request.Content.ReadAsStringAsync()
                        |> Async.AwaitTask

                    return Ok(StringContent c)
                | Stream ->
                    let! s =
                        request.Content.ReadAsStreamAsync()
                        |> Async.AwaitTask

                    return Ok(StreamContent s)
            | false ->
                let! error =
                    request.Content.ReadAsStringAsync()
                    |> Async.AwaitTask

                return Error error
        with
        | :? InvalidOperationException ->
            return Error "The requestUri must be an absolute URI or BaseAddress must be set."
        | :? HttpRequestException ->
            return Error
                       "The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout."
        | :? TaskCanceledException -> return Error "The request failed due to timeout."
    }