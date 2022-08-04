namespace FsMatrix

type MatrixError = {
    errcode: string
    error: string
}

module Matrix =

    open System.Net
    open System.Text.Json
    open System.Text.Json.Serialization
    open FsHttp

    GlobalConfig.Json.defaultJsonSerializerOptions <-
        let options = JsonSerializerOptions()
        options.Converters.Add(JsonFSharpConverter())
        options

    let private getBaseUrl wellKnownUrl =
        http {
            GET wellKnownUrl
        }
        |> Request.sendAsync

    let getBaseUrlFromHomeserverName name = async {
        let! wkResult =
            sprintf "https://%s/.well-known/matrix/client" name
            |> getBaseUrl

        return wkResult
    }

        // |> Async.map (Response.expectHttpStatusCodes [HttpStatusCode.OK])
        // |> Async.map (
        //     function
        //     | Error exp ->

        // )

        // |> Async.map Response.toJson
        // |> Async.map (fun json -> json?``m.homeserver``?base_url.GetString())