@echo off

cd src

dotnet restore
dotnet build

cd ..

cd tools

REM Instrument assemblies inside 'test' folder to detect hits for source files inside 'src' folder
dotnet minicover instrument --workdir ../ --assemblies test/**/bin/**/*.dll --sources src/**/*.cs 

REM Reset hits count in case minicover was run for this project
dotnet minicover reset

cd ..

cd test
dotnet tool install -g foreach
foreach files *.csproj --input _I * dotnet test --no-build _I

cd ..
cd tools

REM Uninstrument assemblies, it's important if you're going to publish or deploy build outputs
dotnet minicover uninstrument --workdir ../

REM Create html reports inside folder coverage-html
dotnet minicover htmlreport --workdir ../ --threshold 50

REM Print console report
REM This command returns failure if the coverage is lower than the threshold
dotnet minicover report --workdir ../ --threshold 50

cd ..