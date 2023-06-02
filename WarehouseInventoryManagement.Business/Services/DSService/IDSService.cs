using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseInventoryManagement.Business.Models;

namespace WarehouseInventoryManagement.Business.Services
{
    public interface IDSService
    {
        Task<bool> ConfigureDevice(DeviceDTO deviceDTO);
    }
}
