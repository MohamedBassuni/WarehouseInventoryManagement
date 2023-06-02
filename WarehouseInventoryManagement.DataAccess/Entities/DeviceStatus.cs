using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WarehouseInventoryManagement.DataAccess.Entities
{
    public class DeviceStatus : EntityBase
    {
        [Required]
        public string Name { get; set; }

    }
}
