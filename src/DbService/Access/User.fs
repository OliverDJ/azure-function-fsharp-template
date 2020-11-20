
namespace DbService.Access

module User =
    open DbRepository
    open FSharp.Control.Tasks.V2.ContextInsensitive
    open System.Linq
    open DbService.Mappers.User

    let findUser (context: TemplateDatabaseContext) identifier = 
        context
            .Users
            .SingleOrDefault(fun u -> u.ID = identifier)

    let applyFOptionLift (f: 'a -> 'b) (identifier: 'a )  =
        identifier
        |> f
        |>  Option.ofObj


    let getUser (context: TemplateDatabaseContext) identifier =
        task{
            let user =
                identifier
                |> applyFOptionLift (findUser context)
                |> Option.map mapDbRepoToDbService
            return user
        }