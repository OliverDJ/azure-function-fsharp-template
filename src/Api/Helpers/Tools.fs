namespace Api.Helpers.Tools


module Serdes =
    open Newtonsoft.Json
    module Json =
        let serialize x = JsonConvert.SerializeObject(x)

module Http =
    open Serdes.Json
    module Respones =
        open Microsoft.AspNetCore.Mvc
        
        let badRequest =
            BadRequestObjectResult("Unfortunatly, we could not find a X-ARR-ClientCert header") 
            :> IActionResult

        let ok x = 
           OkObjectResult( x |> serialize) 
           :> IActionResult
