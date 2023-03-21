using LinqToDB;
using LinqToDB.Configuration;

namespace eshopBackend.DAL.DbSettings;

public class ConnectionStringSettings : IConnectionStringSettings
{
    public string ConnectionString { get; set; }
    public string Name             { get; set; }
    public string ProviderName     { get; set; }
    public bool   IsGlobal         => false;
}

public class DbSettings : ILinqToDBSettings
{
    public IEnumerable<IDataProviderSettings> DataProviders
        => Enumerable.Empty<IDataProviderSettings>();

    public string DefaultConfiguration => "eshopBackendDB";
    public string DefaultDataProvider  => "MySql";

    public IEnumerable<IConnectionStringSettings> ConnectionStrings
    {
        get
        {
            yield return
                new ConnectionStringSettings
                {
                    Name             = "eshopBackendDB",
                    ProviderName     = ProviderName.MySql,
                    ConnectionString =
                        @"connectionstring"
                };
        }
    }
}