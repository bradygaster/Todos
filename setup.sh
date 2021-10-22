#!/bin/bash

echo 'What would you like the base resource name to be?'
read resourceBaseName

if [ -z "$resourceBaseName" ]
then
    echo 'You need to provide a base resource name. Exiting.'
    exit
fi

echo 'What would you like the database username to be?'
read sqlServerDbUsername

if [ -z "$sqlServerDbUsername" ]
then
    echo 'You need to provide a database username. Exiting.'
    exit
fi

echo 'What would you like the database password to be? (will not show on screen)'
read -s sqlServerDbPwd

if [ -z "$sqlServerDbPwd" ]
then
    echo 'You need to provide a database password. Exiting.'
    exit
fi

echo 'Creating resource group '$resourceBaseName
az group create -l westus -n $resourceBaseName
az deployment group create --resource-group $resourceBaseName --template-file resources.bicep --parameters resourceBaseName=$resourceBaseName --parameters sqlUsername=$sqlServerDbUsername --parameters sqlPassword=$sqlServerDbPwd 

echo 'Building API'
dotnet build Todos.API/Todos.API.csproj
dotnet publish Todos.API/Todos.API.csproj -o publish/api

echo 'Building Web UI'
dotnet build Todos.Web/Todos.Web.csproj
dotnet publish Todos.Web/Todos.Web.csproj -o publish/web

echo 'Packaging projects for deployment'
rm *.zip
cd publish/api
zip -r ../../api.zip .

cd ../web
zip -r ../../web.zip .
cd ../..

echo 'Deploying API'
az webapp deploy -n $resourceBaseName'-api' -g $resourceBaseName --src-path api.zip

echo 'Deploying Web UI'
az webapp deploy -n $resourceBaseName'-web' -g $resourceBaseName --src-path web.zip

echo 'Opening Web UI in a browser'
az webapp browse -n $resourceBaseName'-web' -g $resourceBaseName
