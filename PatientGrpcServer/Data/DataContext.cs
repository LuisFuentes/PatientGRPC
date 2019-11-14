using Microsoft.EntityFrameworkCore;
using PatientGrpcServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientGrpcServer.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Models.Patient> Patient { get; set; }
    }
}
