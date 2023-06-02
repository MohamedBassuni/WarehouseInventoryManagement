using System;
using System.ComponentModel.DataAnnotations;
using WarehouseInventoryManagement.DataAccess.Entities;

namespace WarehouseInventoryManagement.Business.Models
{
    public class DeviceConfigurationDTO
    {
        public double Temperature { get; set; }
        public int DeviceStatusId { get; set; }
    }
}