﻿using System;
using System.ComponentModel.DataAnnotations;
using WarehouseInventoryManagement.DataAccess.Entities;

namespace WarehouseInventoryManagement.Business.Models
{
    public class DeviceDTO
    {
        public int Id { get; set; }
        public int Temperature { get; set; }
        public int Pin { get; set; }
        public int DeviceStatusId { get; set; }
        public string DeviceStatus { get; set; }

    }
}