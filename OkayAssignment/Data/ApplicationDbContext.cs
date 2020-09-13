using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OkayAssignment.Models;
 

namespace OkayAssignment.Data
{
    public class ApplicationDbContext : IdentityDbContext<OkayAssignmentUser> 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
         
         DbSet<OkayAssignmentUser> OkayAssignmentUser { get; set; }
        public DbSet<Property> Property { get; set; }
        public DbSet<Transaction> Transaction { get; set; }

    }
}
