# Eshop backend project
Eshop backend with MySQL DB connector - C# .NET project

> Team: Negotiators

| Team members | Main focus |
|---|---|
| Vojtěch Trunda | Data Application Layer |
| Filip Žádník | Application Programming Interface |

## How to run
### Compiled binaries
Should be uploaded by CI to every release tab.

> **Warning**
> You need to make configuration file before you run the project

### Build from source
You can build the project from source by cloning the repo or downloading the packed sources from release tab.

> **Warning**
> You need to make configuration file before you run the project

## DB config
Make a file eshopBackend.DAL/appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=eshopBackend;User Id=eshopBackend;Password=secret;"
  }
}
```

## Migrations
Execute in DAL folder

### Migration create command
```bash
dotnet ef --startup-project ../eshopBackend.DAL/eshopBackend.DAL.csproj migrations add "init" --context DbConnectorFactory --output-dir Migrations --project ../eshopBackend.DAL/eshopBackend.DAL.csproj
```

### Migration update command
```bash
dotnet ef database update
```