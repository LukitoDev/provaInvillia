using LocaGames.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LocaGames.Infra.Data.Context
{
    public class SqlContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder
            //        .UseSqlServer("Data Source=DESKTOP-RKJ2OBH\\SQLEXPRESS;Initial Catalog=LocaGames2;Persist Security Info=True;User ID=sa;Password=1234;Connect Timeout=3000;ConnectRetryCount=0",
            //options => options.EnableRetryOnFailure());
        }

        public SqlContext(DbContextOptions<SqlContext> options)
        : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);
        }

        #region Tables
        public DbSet<Friend> Friends { get; set; }
        public DbSet<BorrowedGame> BorrowedGames { get; set; }
        public DbSet<Game> Games { get; set; }
        #endregion

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}