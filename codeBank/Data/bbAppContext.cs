using codeBank.Model;
using System.Data.Entity;
using System.Data.Entity.Core.Common;

namespace codeBank.Data
    {
    //[DbConfigurationType(typeof(MySqlEFConfiguration))]
    public partial class bbAppContext : DbContext
    {
        public bbAppContext()
            :base("Default")
        {
            //Database.SetInitializer<bbAppContext>(new CreateDatabaseIfNotExists<bbAppContext>());
            Database.SetInitializer<bbAppContext>(new dbHazirla());
        }

        public DbSet<Kategori> Kategories { get; set; }
        public DbSet<Parca> Parcas { get; set; }

    }

    public class SQLiteConfiguration : DbConfiguration
    {
        public SQLiteConfiguration()
        {
            SetProviderFactory("System.Data.SQLite", System.Data.SQLite.SQLiteFactory.Instance);
            SetProviderFactory("System.Data.SQLite.EF6", System.Data.SQLite.EF6.SQLiteProviderFactory.Instance);
            SetProviderServices("System.Data.SQLite", (DbProviderServices)System.Data.SQLite.EF6.SQLiteProviderFactory.Instance.GetService(typeof(DbProviderServices)));
        }
    }

}
