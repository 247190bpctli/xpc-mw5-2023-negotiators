# Eshop backend project
Eshop backend with MySQL DB connector - C# .NET project. Project version A.

> Team: Negotiators

| Team members   | E-mail             | Main focus                        |
|----------------|--------------------|-----------------------------------|
| Vojtěch Trunda | vojta478@gmail.com | Data Application Layer            |
| Filip Žádník   | 246976@vutbr.cz    | Application Programming Interface |

## How to run
### Compiled binaries
Should be uploaded by CI to every release tab.

### Build from source
You can build the project from source by cloning the repo or downloading the packed sources from release tab.

> **Warning**
> You need to configure user secrets and migrate database before you run the project

## User secrets configuration

### DB connection string
Execute in API folder
```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=eshopBackend;User Id=eshopBackend;Password=secret;"
```

## Migrations
Execute in DAL folder

### Migration create command
```bash
dotnet ef migrations add "<name>"
```

### Migration update command
```bash
dotnet ef database update
```