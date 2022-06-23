using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lab01.Models
{
    public class MasterDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        
        public DbSet<ApplicationRole> AspNetRoles { get; set; }
        public DbSet<ApplicationUser> AspNetUsers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<BoardTransaction> BoardTransactions { get; set; }
        
        public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options) 
        {
            
        }
    }
}
