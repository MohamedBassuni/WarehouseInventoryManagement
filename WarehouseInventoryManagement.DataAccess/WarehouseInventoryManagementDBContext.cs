using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using WarehouseInventoryManagement.DataAccess.Entities;

namespace WarehouseInventoryManagement.DataAccess
{
    public class WarehouseInventoryManagementDBContext : DbContext
    {

        public DbSet<DeviceStatus> DeviceStatus { get; set; }
        public DbSet<Device> Devices { get; set; }
        public WarehouseInventoryManagementDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<DeviceStatus>().HasData(
                    new DeviceStatus { Id = 1, Name = "READY" },
                    new DeviceStatus { Id = 2, Name = "ACTIVE" });
            base.OnModelCreating(modelBuilder);
        }
    }
}
