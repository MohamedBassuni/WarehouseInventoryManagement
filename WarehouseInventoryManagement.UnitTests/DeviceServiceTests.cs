using AutoMapper;
using Microsoft.AspNetCore.Routing;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WarehouseInventoryManagement.Business.Models;
using WarehouseInventoryManagement.Business.Services;
using WarehouseInventoryManagement.DataAccess.Entities;
using WarehouseInventoryManagement.DataAccess.Repository;

namespace WarehouseInventoryManagement.UnitTests
{
    [TestClass]
    public class DeviceServiceTests
    {

        private DeviceService deviceService;
        private Mock<IDeviceRepository> deviceRepositoryMock;
        private Mock<IMapper> mapperMock;

        public DeviceServiceTests()
        {
            deviceRepositoryMock = new Mock<IDeviceRepository>();
            mapperMock = new Mock<IMapper>();

            deviceService = new DeviceService(deviceRepositoryMock.Object, mapperMock.Object);
        }

        [TestMethod]
        public async Task Add_ValidDeviceDTO_ReturnsDeviceDTO()
        {

            var deviceCreationDTO = new DeviceCreationDTO
            {
                Pin = "123456",
            };

            var expectedDevice = new Device
            {
                Id = 1,
                Temperature = -1,
                DeviceStatusId = (int)DeviceStatusEnum.READY,
                Pin = deviceCreationDTO.Pin,
            };

            var expectedDeviceDTO = new DeviceDTO
            {
                Id = 1,
                Temperature = -1,
                DeviceStatusId = (int)DeviceStatusEnum.READY,
                Pin = deviceCreationDTO.Pin,
            };

            deviceRepositoryMock.Setup(repo => repo.Get(It.IsAny<Expression<Func<Device, bool>>>()))
                .ReturnsAsync((Device)null);

            deviceRepositoryMock.Setup(repo => repo.Add(It.IsAny<Device>()))
                .ReturnsAsync(expectedDevice);

            mapperMock.Setup(mapper => mapper.Map<Device, DeviceDTO>(It.IsAny<Device>()))
                .Returns(expectedDeviceDTO);


            var result = await deviceService.Add(deviceCreationDTO);


            Assert.AreEqual(expectedDeviceDTO, result);
        }

        [TestMethod]
        public void AddNullDeviceCreationDTO()
        {

            DeviceCreationDTO deviceCreationDTO = null;

            try
            {
                deviceService.Add(deviceCreationDTO);

            }
            catch (Exception ex)
            {
                Assert.AreEqual("Invalid Object", ex.Message);
            }
        }
        [TestMethod]
        public void Add_EmptyPin_ThrowsException()
        {

            var deviceCreationDTO = new DeviceCreationDTO
            {
                Pin = "",
            };

            try
            {
                deviceService.Add(deviceCreationDTO);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Pin should not be empty", ex.Message);
            }
        }

        [TestMethod]
        public async Task Update_ValidIdAndDeviceDTO_ReturnsTrue()
        {

            int deviceId = 1;
            var deviceUpdatingDTO = new DeviceUpdatingDTO
            {
                Pin = "123456",
                Temperature = 25,
            };

            var existingDevice = new Device
            {
                Id = deviceId,
                Temperature = 20,
                DeviceStatusId = (int)DeviceStatusEnum.ACTIVE,
                Pin = deviceUpdatingDTO.Pin,
            };

            deviceRepositoryMock.Setup(repo => repo.Get(deviceId))
                .ReturnsAsync(existingDevice);


            var result = await deviceService.Update(deviceId, deviceUpdatingDTO);
            Assert.AreEqual(deviceUpdatingDTO.Temperature, existingDevice.Temperature);

        }

        [TestMethod]
        public async Task Update_InvalidId_ThrowsException()
        {
            try
            {
                int deviceId = 1;
                DeviceUpdatingDTO deviceUpdatingDTO = new DeviceUpdatingDTO();

                deviceRepositoryMock.Setup(repo => repo.Get(deviceId))
                    .ReturnsAsync((Device)null);
                deviceService.Update(deviceId, deviceUpdatingDTO);

            }
            catch (Exception ex)
            {
                Assert.AreEqual("Pin should not be empty", ex.Message);
            }


        }

        [TestMethod]
        public async Task Update_NullDeviceDTO_ThrowsException()
        {

            int deviceId = 1;
            DeviceUpdatingDTO deviceUpdatingDTO = null;


            await Assert.ThrowsExceptionAsync<Exception>(() => deviceService.Update(deviceId, deviceUpdatingDTO));

        }

        [TestMethod]
        public async Task Update_EmptyPin_ThrowsException()
        {

            int deviceId = 1;
            var deviceUpdatingDTO = new DeviceUpdatingDTO
            {
                Pin = "",
                Temperature = 25,
            };


            await Assert.ThrowsExceptionAsync<Exception>(() => deviceService.Update(deviceId, deviceUpdatingDTO));

        }

        [TestMethod]
        public async Task Remove_ValidDeviceId_ReturnsTrue()
        {

            int deviceId = 1;

            var existingDevice = new Device
            {
                Id = deviceId,
                Temperature = 20,
                DeviceStatusId = (int)DeviceStatusEnum.ACTIVE,
                Pin = "123456",
            };

            deviceRepositoryMock.Setup(repo => repo.Get(deviceId))
                .ReturnsAsync(existingDevice);

            deviceRepositoryMock.Setup(repo => repo.Remove(existingDevice))
                .ReturnsAsync(true);


            var result = await deviceService.Remove(deviceId);


            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task Remove_InvalidDeviceId_ThrowsException()
        {

            int deviceId = 1;

            deviceRepositoryMock.Setup(repo => repo.Get(deviceId))
                .ReturnsAsync((Device)null);


            await Assert.ThrowsExceptionAsync<Exception>(() => deviceService.Remove(deviceId));
        }

        [TestMethod]
        public async Task GetAllActiveDevices_ReturnsListOfActiveDevices()
        {

            var activeDevices = new List<Device>
    {
        new Device { Id = 1, Temperature = 20, DeviceStatusId = (int)DeviceStatusEnum.ACTIVE, Pin = "123456" },
        new Device { Id = 2, Temperature = 25, DeviceStatusId = (int)DeviceStatusEnum.ACTIVE, Pin = "654321" }
    };

            var expectedDeviceDTOs = new List<DeviceDTO>
    {
        new DeviceDTO { Id = 1, Temperature = 20, DeviceStatusId =(int)DeviceStatusEnum.ACTIVE, Pin = "123456" },
        new DeviceDTO { Id = 2, Temperature = 25, DeviceStatusId =(int)DeviceStatusEnum.ACTIVE, Pin = "654321" }
    };

            deviceRepositoryMock.Setup(repo => repo.GetAll(It.IsAny<Expression<Func<Device, bool>>>()))
                .ReturnsAsync(activeDevices);

            mapperMock.Setup(mapper => mapper.Map<List<Device>, List<DeviceDTO>>(activeDevices))
                .Returns(expectedDeviceDTOs);


            var result = await deviceService.GetAllActiveDevices();


            CollectionAssert.AreEqual(expectedDeviceDTOs, result);
        }


    }

}