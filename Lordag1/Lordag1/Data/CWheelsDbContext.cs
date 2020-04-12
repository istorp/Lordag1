using Lordag1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lordag1.Data
{
    public class CWheelsDbContext : DbContext
    {
        public CWheelsDbContext(DbContextOptions<CWheelsDbContext> options) : base(options)
        {

        }
        public DbSet<Vehicle> Vehicles { get; set; }
    }
}
