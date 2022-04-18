using API.Models;
using Microsoft.EntityFrameworkCore;
using System;


namespace API.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<University> University { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<Profiling> Profiling { get; set; }
        public DbSet<AccountRole> AccountRole { get; set; }
        public DbSet<Role> Role { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // one to one relationship
            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Account)
                .WithOne(b => b.Employee)
                .HasForeignKey<Account>(b => b.NIK);
            // one to one relationship
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Profiling)
                .WithOne(p =>p.Account)
                .HasForeignKey<Profiling>(p => p.NIK);
            // many to one relationship
            modelBuilder.Entity<Education>()
                .HasMany(e => e.Profiling)
                .WithOne(p => p.Education);
            // many to one relationship
            modelBuilder.Entity<University>()
                .HasMany(u => u.Education)
                .WithOne(e => e.University);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.AccountRole)
                .WithOne(a => a.Account);

            modelBuilder.Entity<Role>()
                .HasMany(r => r.AccountRole)
                .WithOne(a => a.Role);
        }
    }
}
