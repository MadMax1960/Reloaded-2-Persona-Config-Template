# Set Working Directory
Split-Path $MyInvocation.MyCommand.Path | Push-Location
[Environment]::CurrentDirectory = $PWD

Remove-Item "$env:RELOADEDIIMODS/NaoSmiley/*" -Force -Recurse
dotnet publish "./NaoSmiley.csproj" -c Release -o "$env:RELOADEDIIMODS/NaoSmiley" /p:OutputPath="./bin/Release" /p:ReloadedILLink="true"

# Restore Working Directory
Pop-Location