using eshopBackend.DAL.Entities;
using LinqToDB;

namespace eshopBackend.DAL.DbSettings;

public class DbMapper : LinqToDB.Data.DataConnection
{

        public DbMapper() : base("EshopBackendDb") { }

        public ITable<EntityProduct>  Product  => this.GetTable<EntityProduct>();
        public ITable<EntityCategory> Category => this.GetTable<EntityCategory>();



}