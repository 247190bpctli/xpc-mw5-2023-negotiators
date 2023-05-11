# Eshop backend project
Eshop backend with MySQL DB connector - C# .NET project. Project version A.

> Team: Negotiators

| Team members   | E-mail             | Main focus                        |
|----------------|--------------------|-----------------------------------|
| Vojtěch Trunda | vojta478@gmail.com | Data Application Layer            |
| Filip Žádník   | 246976@vutbr.cz    | Application Programming Interface |

## How to run
### Docker (easiest)
You can build and run docker image using the following commands.
1. Clone the repository using GIT
2. Create MySQL database and apply migrations using the command below
3. Copy /eshopBackend.API/appsettings.json to downloaded binary folder and add your DB connection string
4. Run following commands to build and run the container
```bash
docker build -t eshopbackend .
docker run --rm -p <external port>:80 -v <config location>:/app/appsettings.json eshopbackend
```

### Compiled binaries
Should be uploaded by CI to every release tab.
1. Download binaries from release tab, current supported platforms are 64-bit Windows and Linux
2. Clone the repository using GIT
3. Create MySQL database and apply migrations using the command below
4. Copy /eshopBackend.API/appsettings.json to downloaded binary folder and add your DB connection string

### Build from source
You can build the project from source by cloning the repo or downloading the packed sources from release tab.
1. Clone the repository using GIT
2. Create MySQL database and apply migrations using the command below
3. Configure user secrets (DB connection string)
   - execute in API folder
    ```bash
    dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=eshopBackend;User Id=eshopBackend;Password=secret;"
    ```
## Configuration entries
You can configure this application in appsettings.json file located in eshopBackend.API folder.
```yaml
{
   "Logging": {
      "LogLevel": { //log levels for classes
         "Default": "Debug",
         "Microsoft.AspNetCore": "Warning"
      }
   },
   "AllowedHosts": "*", //hosts allowed to connect
   "ConnectionStrings": {
      "DefaultConnection": "" //MySQL DB connection string
   },
   "Cart": {
      "RemovalInterval": 5, //how often are carts removed [minutes]
      "MaxAge": 24 //how long can carts last before deletion [hours]
   },
   "Seeds": {
      "SeedMockData": true, //enable seeding (when migrating)
      "DataAmount": 5 //how much records to seed
   }
}
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