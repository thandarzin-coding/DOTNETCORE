using DOTNETCORE.RestApi.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTNETCORE.RestApi.Db
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "TestDb",
                UserID = "sa",
                Password = "sa@123",
                TrustServerCertificate = true
            };
            optionsBuilder.UseSqlServer(_sqlConnectionStringBuilder.ConnectionString);
        }

        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
