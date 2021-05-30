#!/bin/bash

# TODO set version dynamically.
# VERSION = 0.1.2

dotnet pack --configuration Release -o ../_Releases/FUlit/0.1.2/
dotnet nuget push ../_Releases/FUlit/0.1.2/FUtil.0.1.2.nupkg --source "github"
