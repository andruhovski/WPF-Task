using System.Data.Common;
using System.Data.Entity;
using MySql.Data.EntityFramework;

namespace DataLayer
{
    // Code-Based Configuration and Dependency resolution
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class CardsCatalogContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<CardResource> CardResources { get; set; }

        public CardsCatalogContext() : base("CardContext")
        {

        }

        // Constructor to use on a DbConnection that is already opened
        public CardsCatalogContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {

        }

    }

}
