#r "paket:
nuget Fake.Core.Target 
nuget Fake.IO.FileSystem
nuget Fake.IO.Zip 
nuget Fake.DotNet.Cli
//"
#load "./.fake/build.fsx/intellisense.fsx"

open Fake.Core
open Fake.IO
open Fake.Core.TargetOperators
open Fake.IO.Globbing.Operators
open Fake.DotNet
open System.IO

let appName = "insert-appname"
let sourceDir = __SOURCE_DIRECTORY__
let slnLocation = sourceDir + "\\..\\insert-sln-name.sln"

let nuget = sprintf "%s\\..\\nuget.config" sourceDir

let buildDir = sprintf "%s\\build" sourceDir
let dotnetDir = sprintf "%s\\dotnet" buildDir
let provdir = sprintf "%s\\provision" sourceDir

let rootDir = sourceDir + "\\..\\src"
let rec allFiles dirs =
    if Seq.isEmpty dirs then Seq.empty else
        seq { yield! dirs |> Seq.collect Directory.EnumerateFiles
              yield! dirs |> Seq.collect Directory.EnumerateDirectories |> allFiles }

Target.create "Clean" <|fun _ ->
    Shell.cleanDir buildDir

Target.create "Build" <|fun _ ->
    DotNet.publish (fun opt -> { 
        opt with             
            OutputPath = Some dotnetDir
            Configuration = DotNet.BuildConfiguration.Release
            }) slnLocation


Target.create "Artifact" ( fun _ ->
    let preZip = sprintf "%s\\preZip" buildDir
    let artifactDir = sprintf "%s\\artifacts" buildDir
    let artifactFilename = sprintf "%s\\%s.zip" artifactDir appName
    Shell.mkdir preZip
    Shell.mkdir artifactDir
    Shell.copyDir preZip dotnetDir (fun _ -> true)

    !! (sprintf "%s/**/*.*" preZip)
    |> Zip.zip preZip artifactFilename

    Shell.copyDir artifactDir provdir (fun _ -> true) // Copy all from provdir to artifact folder   
    
)

Target.create "Restore" <|fun _ ->
    DotNet.restore(fun x -> {
        x with ConfigFile= Some nuget
        }) slnLocation

// Dependencies
open Fake.Core.TargetOperators

"Clean"
    ==> "Restore"
    ==> "Build"  
    ==> "Artifact"

// start build
Target.runOrDefault "Artifact"