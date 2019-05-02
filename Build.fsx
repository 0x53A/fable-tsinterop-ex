#r "paket: 
storage: none
source https://api.nuget.org/v3/index.json

nuget FSharp.Core 4.3.4 // https://github.com/fsharp/FAKE/issues/2001
nuget Fake.Core.Target
nuget Fake.DotNet.Cli
nuget Fake.IO.FileSystem
nuget Fake.BuildServer.AppVeyor
//"
#load "./.fake/build.fsx/intellisense.fsx"

#if !FAKE
#r "netstandard"
#endif

open System
open System.Diagnostics

open Fake.Core
open Fake.DotNet
open Fake.IO
open System.Threading
open System.IO
open Fake.Core

// Init buildserver
Fake.Core.BuildServer.install [ Fake.BuildServer.AppVeyor.Installer ]

let (@@) a b = Path.combine a b

let root = __SOURCE_DIRECTORY__

let inline withWorkDir wd = DotNet.Options.withWorkingDirectory wd


let justBuild() =
    DotNet.build (fun o -> { o with Configuration = DotNet.BuildConfiguration.Release }) (root @@ "src\\Fable.TSInterop.Ex\\Fable.TSInterop.Ex.fsproj")

let publishToNuget(v:string) =
    // dotnet pack does a build
    DotNet.pack (fun o ->
        { o with    Configuration = DotNet.BuildConfiguration.Release
                    OutputPath = Some (root @@ "nupkg")
                    MSBuildParams = { MSBuild.CliArguments.Create() with Properties = ["Version",v; "PackageVersion",v] }
        }) (root @@ "src\\Fable.TSInterop.Ex\\Fable.TSInterop.Ex.fsproj")
    let nupkgs = System.IO.Directory.GetFiles(root @@ "nupkg", "*.nupkg", SearchOption.AllDirectories)
    match nupkgs with
    | [| |] -> failwith "found no nupkgs"
    | [| f |] ->
        DotNet.nugetPush (fun o -> { o with
                                        PushParams = { o.PushParams with
                                                                      Source = Some "https://api.nuget.org/v3/index.json"
                                                                      ApiKey = Some (Fake.Core.Environment.environVarOrFail "NUGET_API_KEY") } }) f
    | _ -> failwithf "found more than one nupkg"



Target.create "BuildRelease" (fun _ ->
    justBuild()
)

Target.create "AppVeyor" (fun _ ->
    if not (Fake.BuildServer.AppVeyor.detect()) then
        failwith "expected to be run on appveyor"

    if Fake.BuildServer.AppVeyor.Environment.RepoTag then
        let tagName = Fake.BuildServer.AppVeyor.Environment.RepoTagName
        if tagName.StartsWith("release-") then
            let version = tagName.Substring("release-".Length)
            publishToNuget(version)
        else if tagName.StartsWith("prerelease-") then
            let version = tagName.Substring("prerelease-".Length)
            publishToNuget(version)
        else
            // other, unknown tag
            justBuild()
    else
        // no tag, just build
        justBuild()
)

Target.runOrDefault "BuildRelease"
