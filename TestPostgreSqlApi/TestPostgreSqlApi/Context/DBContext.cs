using Microsoft.EntityFrameworkCore;
using TestPostgreSqlApi.Entities;

namespace TestPostgreSqlApi.Context
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public DbSet<ClienteEntity> Clientes { get; set; }
    }
}