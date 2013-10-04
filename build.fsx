#load "tools/includes.fsx"
open IntelliFactory.Build

let bt =
    BuildTool().PackageId("WebSharper.Google.Maps", "2.5")
        .References(fun r -> [r.Assembly "System.Web"])

let main =
    bt.WebSharper.Extension("IntelliFactory.WebSharper.Google.Maps")
        .SourcesFromProject()

let test =
    bt.WebSharper.HtmlWebsite("IntelliFactory.WebSharper.Google.Maps.Tests")
        .SourcesFromProject()
        .References(fun r -> [r.Project main])

bt.Solution [
    main
    test

    bt.NuGet.CreatePackage()
        .Configure(fun c ->
            { c with
                Title = Some "WebSharper.Google.Maps-3.13"
                LicenseUrl = Some "http://websharper.com/licensing"
                ProjectUrl = Some "https://github.com/intellifactory/websharper.google.maps"
                Description = "WebSharper Extensions for Google Maps 3.13"
                RequiresLicenseAcceptance = true })
        .Add(main)

]
|> bt.Dispatch
