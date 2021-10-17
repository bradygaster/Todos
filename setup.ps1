param (
    $resourceBaseName="todos$( Get-Random -Maximum 1000)",
    $sqlServerDbUsername='todos',
    $sqlServerDbPwd='pass4Sql!4242',
    $location='westus'
)

az group create -l westus -n $resourceBaseName
az deployment group create --resource-group $resourceBaseName --template-file resources.bicep --parameters resourceBaseName=$resourceBaseName --parameters sqlUsername=$sqlServerDbUsername --parameters sqlPassword=$sqlServerDbPwd 

dotnet build Todos.API\Todos.API.csproj
dotnet publish Todos.API\Todos.API.csproj -o publish\api
if (Test-Path .\api.zip) {
    Remove-Item .\api.zip
}
Get-ChildItem -Path .\publish\api\ | Compress-Archive -DestinationPath api.zip
az webapp deploy -n "$($resourceBaseName)-api" -g $resourceBaseName --src-path api.zip

dotnet build Todos.Web\Todos.Web.csproj
dotnet publish Todos.Web\Todos.Web.csproj -o publish\web
if (Test-Path .\web.zip) {
    Remove-Item .\web.zip
}
Get-ChildItem -Path .\publish\web\ | Compress-Archive -DestinationPath web.zip
az webapp deploy -n "$($resourceBaseName)-web" -g $resourceBaseName --src-path web.zip

az webapp browse -n "$($resourceBaseName)-api" -g $resourceBaseName
az webapp browse -n "$($resourceBaseName)-web" -g $resourceBaseName
