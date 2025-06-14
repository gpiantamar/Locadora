using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CarLoc.Models;

namespace CarLoc.Data
{
    public class CarLocContext : DbContext
    {
        public CarLocContext (DbContextOptions<CarLocContext> options)
            : base(options)
        {
        }

        public DbSet<CarLoc.Models.Cliente> Cliente { get; set; } = default!;
        public DbSet<CarLoc.Models.Veiculo> Veiculo { get; set; } = default!;
        public DbSet<CarLoc.Models.Status> Status { get; set; } = default!;
        public DbSet<CarLoc.Models.Categoria> Categoria { get; set; } = default!;
        public DbSet<CarLoc.Models.Contrato> Contrato { get; set; } = default!;
    }
}
