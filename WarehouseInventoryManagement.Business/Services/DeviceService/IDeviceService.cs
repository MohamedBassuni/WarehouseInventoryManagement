using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseInventoryManagement.Business.Models;

namespace WarehouseInventoryManagement.Business.Services
{
    public interface IDeviceService
    {
        Task<DeviceDTO> Add(DeviceCreationDTO deviceDTO);
        Task<bool> Update(int id, DeviceUpdatingDTO deviceDTO);
        Task<bool> Remove(int deviceId);
        Task<List<DeviceDTO>> GetAllActiveDevices();
    }
}
