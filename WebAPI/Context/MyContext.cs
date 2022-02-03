using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Model;

namespace WebAPI.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
        public DbSet<Employee> Employees{set;get;}

        public DbSet<Account> Accounts { set; get; }
        public DbSet<Profilling> Profillings { set; get; }
        public DbSet<University> Universities { set; get; }
        public DbSet<Education> Educations { set; get; }
        public DbSet<AccountRole> AccountRoles { set; get; }
        public DbSet<Role> Roles { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Account)
                .WithOne(b => b.Employee)
                .HasForeignKey<Account>(b => b.NIK);

            modelBuilder.Entity<Account>()
               .HasOne(a => a.Profilling)
               .WithOne(b => b.Account)
               .HasForeignKey<Profilling>(b => b.NIK);

            modelBuilder.Entity<Education>()
                .HasMany(c => c.Profillings)
                .WithOne(e => e.Education);

            modelBuilder.Entity<University>()
                .HasMany(c => c.Educations)
                .WithOne(e => e.University);

            modelBuilder.Entity<Account>()
                .HasMany(c=>c.AccountRole)
                .WithOne(e=> e.Account);

            modelBuilder.Entity<Role>()
              .HasMany(c => c.AccountRole)
              .WithOne(e => e.Role);
        }
    }
}
