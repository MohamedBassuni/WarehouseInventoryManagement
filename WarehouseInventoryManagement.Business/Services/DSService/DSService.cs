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
        public async Task<DeviceDTO> Config(DeviceDTO deviceDTO)
        {
            if (deviceDTO == null)
                throw new Exception("Invalid Object");
            if (string.IsNullOrEmpty(deviceDTO.Pin))
                throw new Exception("Pin should not be empty");

            var device = await this.deviceRepository.Get(s => s.Pin == deviceDTO.Pin);
            if (device != null)
                throw new Exception("Device with same PIN already Exist");

            var newDevice = new Device()
            {
                Temperature = -1,
                DeviceStatusId = (int)Models.DeviceStatus.READY,
                Pin = deviceDTO.Pin,
            };
            return mapper.Map<Device, DeviceDTO>(await this.deviceRepository.Add(newDevice));
        }
        public async Task<bool> ConfigureDevice(DeviceDTO deviceDTO)
        {
            if (deviceDTO == null)
                throw new Exception("Invalid Object");

            if (deviceDTO.Temperature < 0 || deviceDTO.Temperature > 10)
                throw new Exception("Temperature should be between (0 to 10).");

            if (deviceDTO.DeviceStatusId != (int)Models.DeviceStatus.ACTIVE)
                throw new Exception("Device status should be Active");

            var device = await this.deviceRepository.Get(deviceDTO.Id);

            if (device == null)
                throw new Exception("Device with this Id not");

            device.Temperature = deviceDTO.Temperature;
            device.DeviceStatusId = deviceDTO.DeviceStatusId;

            return await this.deviceRepository.Update(device);
        }

    }
}
