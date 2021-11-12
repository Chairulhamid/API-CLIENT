using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Context
{
    public class MyContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {


        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Education> Educations{ get; set; }
        public DbSet<Profilling> Profillings{ get; set; }
        public DbSet<University> Universities{ get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //One to one => Account to Employee
            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Account)
                .WithOne(b => b.Employee)
                .HasForeignKey<Account>(b => b.NIK);

            //One to one => Account to Profilling
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Profilling)
                .WithOne(b => b.Account)
                .HasForeignKey<Profilling>(b => b.NIK);

            //Many  to Many => Univetsity to Education
            modelBuilder.Entity<University>()
                .HasMany(c => c.Educations)
                .WithOne(e => e.University);

            //One to Many => Education to Profilling
            modelBuilder.Entity<Education>()
              .HasMany(c => c.Profillings)
              .WithOne(e => e.Education);

            //One to Many => Education to Profilling
            modelBuilder.Entity<Education>()
              .HasMany(c => c.Profillings)
              .WithOne(e => e.Education);

            modelBuilder.Entity<AccountRole>()
                 .HasKey(bc => new { bc.NIK, bc.Role_Id });
            modelBuilder.Entity<AccountRole>()
                .HasOne(bc => bc.Account)
                .WithMany(b => b.AccountRoles)
                .HasForeignKey(bc => bc.NIK);
            modelBuilder.Entity<AccountRole>()
                .HasOne(bc => bc.Role)
                .WithMany(c => c.AccountRoles)
                .HasForeignKey(bc => bc.Role_Id);
        }
    }
    
}
