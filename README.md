# eshop-backend
Eshop backend with MySQL DB connector - C# .NET project

## DB config
Make a file eshopBackend.DAL/appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=eshopBackend;User Id=eshopBackend;Password=secret;"
  }
}
```

### Migrations
Migration create command
```bash
dotnet ef --startup-project ../eshopBackend.DAL/eshopBackend.DAL.csproj migrations add "init"--context DbConnector --output-dir Migrations --project ../eshopBackend.DAL/eshopBackend.DAL.csproj
```

Migration update command
```bash
dotnet ef database update
```