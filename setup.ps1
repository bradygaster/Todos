param (
    $resourceBaseName="todos$( Get-Random -Maximum 1000)",
    $sqlServerDbUsername='todos',
    $sqlServerDbPwd='pass4Sql!4242',
    $location='westus'
)

if (Test-Path .\api.zip) {
    Remove-Item .\api.zip
}

try {
    dotnet tool install --global Zipper --version 1.0.1
} catch {
    Write-Host "Environment already has Zipper installed. Yay!" -ForegroundColor Yellow
}

dotnet build Todos.API\Todos.API.csproj
dotnet publish Todos.API\Todos.API.csproj -o publish\api
zipper compress -i publish\api -o api.zip

az group create -l westus -n $resourceBaseName
az deployment group create --resource-group $resourceBaseName --template-file resources.bicep --parameters resourceBaseName=$resourceBaseName --parameters sqlUsername=$sqlServerDbUsername --parameters sqlPassword=$sqlServerDbPwd 
az webapp deploy -n "$($resourceBaseName)-api" -g $resourceBaseName --src-path api.zip
az webapp browse -n "$($resourceBaseName)-api" -g $resourceBaseName
