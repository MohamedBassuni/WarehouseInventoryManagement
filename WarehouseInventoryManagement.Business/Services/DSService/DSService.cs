using AutoMapper;
using WarehouseInventoryManagement.Business.Models;
using WarehouseInventoryManagement.DataAccess.Entities;
using WarehouseInventoryManagement.DataAccess.Repository;

namespace WarehouseInventoryManagement.Business.Services
{
    public class DSService : IDSService
    {
        private IDeviceRepository deviceRepository;
        private IMapper mapper;

        public DSService(IDeviceRepository deviceRepository, IMapper mapper)
        {
            this.deviceRepository = deviceRepository;
            this.mapper = mapper;
        }

        public async Task<bool> ConfigureDevice(int id, DeviceConfigurationDTO deviceDTO)
        {
            if (deviceDTO == null)
                throw new Exception("Invalid Object");

            if (deviceDTO.Temperature < 0 || deviceDTO.Temperature > 10)
                throw new Exception("Temperature should be between (0 to 10).");

            if (deviceDTO.DeviceStatusId != (int)Models.DeviceStatusEnum.ACTIVE)
                throw new Exception("Device status should be Active");

            var device = await this.deviceRepository.Get(id);

            if (device == null)
                throw new Exception("Device with this Id not found");

            device.Temperature = deviceDTO.Temperature;
            device.DeviceStatusId = deviceDTO.DeviceStatusId;

            return await this.deviceRepository.Update(device);
        }

    }
}
