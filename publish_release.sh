# TODO set version dynamically.
VERSION = "0.1.2"

dotnet pack --configuration Release -o ../_Releases/FUlit/$VERSION/
dotnet nuget push ../_Releases/FUlit/$VERSION/FUtil.$VERSION.nupkg --source "github"