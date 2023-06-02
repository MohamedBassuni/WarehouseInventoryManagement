using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseInventoryManagement.Business.Models;

namespace WarehouseInventoryManagement.Business.Services
{
    public interface IDeviceService
    {
        Task<DeviceDTO> Add(DeviceDTO deviceDTO);
        Task<bool> Update(DeviceDTO deviceDTO);
        Task<bool> Remove(int deviceId);
        Task<List<DeviceDTO>> GetAllDevices();
    }
}
