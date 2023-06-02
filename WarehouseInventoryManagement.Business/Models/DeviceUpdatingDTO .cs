using System;
using System.ComponentModel.DataAnnotations;
using WarehouseInventoryManagement.DataAccess.Entities;

namespace WarehouseInventoryManagement.Business.Models
{
    public class DeviceUpdatingDTO
    {
        public double Temperature { get; set; }
        public string Pin { get; set; }

    }
}