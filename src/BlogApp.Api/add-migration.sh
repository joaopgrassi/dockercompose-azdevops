#!/bin/bash

echo Enter migration name

read -p 'MigrationName: ' migrationName

if [ -z "$migrationName" ]
then
    echo "Migration name is required"
    exit 1;
fi

dotnet ef migrations add $migrationName --project ../BlogApp.Data
