using AutoMapper;
using WarehouseInventoryManagement.Business.Models;
using WarehouseInventoryManagement.DataAccess.Entities;
using WarehouseInventoryManagement.DataAccess.Repository;

namespace WarehouseInventoryManagement.Business.Services
{
    public class DeviceService : IDeviceService
    {
        private IDeviceRepository deviceRepository;
        private IMapper mapper;

        public DeviceService(IDeviceRepository deviceRepository, IMapper mapper)
        {
            this.deviceRepository = deviceRepository;
            this.mapper = mapper;
        }
        public async Task<DeviceDTO> Add(DeviceCreationDTO deviceDTO)
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
                DeviceStatusId = (int)DeviceStatusEnum.READY,
                Pin = deviceDTO.Pin,
            };
            return mapper.Map<Device, DeviceDTO>(await this.deviceRepository.Add(newDevice));
        }
        public async Task<bool> Update(int id, DeviceUpdatingDTO deviceDTO)
        {

            if (deviceDTO == null)
                throw new Exception("Invalid Object");

            if (string.IsNullOrEmpty(deviceDTO.Pin))
                throw new Exception("Pin should not be empty");

            var device = await this.deviceRepository.Get(id);

            if (device == null)
                throw new Exception("Device with this Id not");

            if (device.Pin != deviceDTO.Pin)
                throw new Exception("Invalid Pin");

            if (device.DeviceStatusId != (int)DeviceStatusEnum.ACTIVE)
                throw new Exception("Device temperature can't change until device is activated ");

            device.Temperature = deviceDTO.Temperature;

            return await this.deviceRepository.Update(device);
        }
        public async Task<bool> Remove(int deviceId)
        {
            var device = await this.deviceRepository.Get(deviceId);

            if (device == null)
                throw new Exception("Invalid Device Id");

            return await this.deviceRepository.Remove(device);
        }
        public async Task<List<DeviceDTO>> GetAllActiveDevices()
        {
            return mapper.Map<List<Device>, List<DeviceDTO>>(await this.deviceRepository.GetAll(s => s.DeviceStatusId == (int)Models.DeviceStatusEnum.ACTIVE));
        }
    }
}
