![.NET](https://github.com/chrishanzlik/FoodVault/workflows/.NET/badge.svg)

--------

TBA

--------

## Adding Migrations
The storage bounded context serves as example:
1. Ensure dotnet EF tooling is installed with
    ```dotnet tool install --global dotnet-ef```
2. Navigate to the **Modules/Storage/Infrastructure** project folder
3. Execute the migration command
    ```dotnet ef migrations add "migration-name" --startup-project ..\..\..\Api -o Database\Migrations```
