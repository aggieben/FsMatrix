namespace FsMatrix

type MatrixError = {
    errcode: string
    error: string
}

module Matrix =

    open System.Text.Json
    open System.Text.Json.Serialization
    open FsHttp
    
    GlobalConfig.Json.defaultJsonSerializerOptions <-
        let options = JsonSerializerOptions()
        options.Converters.Add(JsonFSharpConverter())
        options

    let getBaseUrlFromHomeserverName name =
        http {
            GET (sprintf "https://%s/.well-known/matrix/client" name) 
        }
        |> Request.sendAsync
        |> Async.map Response.toJson
        |> Async.map (fun json -> (json?``m.homeserver``).GetString())