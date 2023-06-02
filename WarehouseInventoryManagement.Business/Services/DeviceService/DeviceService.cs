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
        public async Task<DeviceDTO> Add(DeviceDTO deviceDTO)
        {
            var device = new Device()
            {
                Temperature = -1,
                DeviceStatusId = (int)Models.DeviceStatus.READY,
                Pin = deviceDTO.Pin,
            };
            return mapper.Map<Device, DeviceDTO>(await this.deviceRepository.Add(device));
        }
        public async Task<bool> Update(DeviceDTO deviceDTO)
        {
            var device = await this.deviceRepository.Get(deviceDTO.Id);

            if (device == null)
                throw new Exception("Invalid Device Id");

            device.Temperature = deviceDTO.Temperature;
            device.DeviceStatusId = deviceDTO.DeviceStatusId;

            return await this.deviceRepository.Update(device);
        }
        public async Task<bool> Remove(int deviceId)
        {
            var device = await this.deviceRepository.Get(deviceId);

            if (device == null)
                throw new Exception("Invalid Device Id");

            return await this.deviceRepository.Remove(device);
        }
        public async Task<List<DeviceDTO>> GetAllDevices()
        {
            return mapper.Map<List<Device>, List<DeviceDTO>>(await this.deviceRepository.GetAll());
        }
    }
}
