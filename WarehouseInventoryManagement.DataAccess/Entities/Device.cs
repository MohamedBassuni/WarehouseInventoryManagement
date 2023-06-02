using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseInventoryManagement.DataAccess.Entities
{
    public class Device : EntityBase
    {

        [Required]        
        public int Temperature { get; set; }
        [Required]
        public int Pin { get; set; }
        [Required]
        public int DeviceStatusId { get; set; }
        public DeviceStatus DeviceStatus { get; set; }
    }
}
