using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OkayAssignment.Models;

namespace OkayAssignment.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
          : base(options)
        { }
        public DbSet<Property> Property { get; set; }
        public DbSet<Transaction> Transaction { get; set; } 
    }
}
