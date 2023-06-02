using Microsoft.EntityFrameworkCore;
using System;
using WarehouseInventoryManagement.DataAccess;
using WarehouseInventoryManagement.DataAccess.Entities;
using WarehouseInventoryManagement.DataAccess.Repository;

namespace WarehouseInventoryManagement.UnitTests
{
    [TestClass]
    public class DeviceRepositoryTests
    {


        private readonly WarehouseInventoryManagementDBContext dbContext;
        private IDeviceRepository deviceRepository;


        public DeviceRepositoryTests()
        {
            DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(
                    Guid.NewGuid().ToString()
            );

            dbContext = new WarehouseInventoryManagementDBContext(dbOptions.Options);
            deviceRepository = new DeviceRepository(dbContext);
        }

        [TestMethod]
        public async Task CreateNewDevice()
        {

            var device = new Device
            {
                Temperature = 25.5,
                Pin = "123456",
                DeviceStatusId = 1
            };

            var result = await deviceRepository.Add(device);
            Assert.IsNotNull(result);
            Assert.IsTrue(dbContext.Set<Device>().Contains(result));
        }

        [TestMethod]
        public async Task AddingThenGetting()
        {
            var device = new Device
            {
                Temperature = 25.5,
                Pin = "123456",
                DeviceStatusId = 1
            };
            dbContext.Set<Device>().Add(device);
            await dbContext.SaveChangesAsync();


            var result = await deviceRepository.Get(device.Id);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetWithPredicate()
        {

            var device = new Device
            {
                Temperature = 25.5,
                Pin = "123456",
                DeviceStatusId = 1
            };
            dbContext.Set<Device>().Add(device);
            await dbContext.SaveChangesAsync();

            var result = await deviceRepository.Get(d => d.Pin == "123456");

            Assert.IsNotNull(result);
            Assert.AreEqual(device, result);
        }

        [TestMethod]
        public async Task GetAll()
        {

            var devices = new List<Device>
        {
            new Device
            {
                Temperature = 25.5,
                Pin = "123456",
                DeviceStatusId = 1
            },
            new Device
            {
                Temperature = 30.0,
                Pin = "654321",
                DeviceStatusId = 2
            }
        };
            dbContext.Set<Device>().AddRange(devices);
            await dbContext.SaveChangesAsync();

            
            var result = await deviceRepository.GetAll();

            
            Assert.IsNotNull(result);
            Assert.AreEqual(devices.Count, result.Count);
            Assert.IsTrue(devices.All(d => result.Contains(d)));
        }

        [TestMethod]
        public async Task GetAllWithPredicate()
        {
            
            var devices = new List<Device>
        {
            new Device
            {
                Temperature = 25.5,
                Pin = "123456",
                DeviceStatusId = 1
            },
            new Device
            {
                Temperature = 30.0,
                Pin = "654321",
                DeviceStatusId = 2
            }
        };
            dbContext.Set<Device>().AddRange(devices);
            await dbContext.SaveChangesAsync();

            
            var result = await deviceRepository.GetAll(d => d.Temperature > 20.0);

            
            Assert.IsNotNull(result);
            Assert.AreEqual(devices.Count, result.Count);
            Assert.IsTrue(devices.All(d => result.Contains(d)));
        }

        [TestMethod]
        public async Task UpdateDevice()
        {
            
            var device = new Device
            {
                Temperature = 25.5,
                Pin = "123456",
                DeviceStatusId = 1
            };
            dbContext.Set<Device>().Add(device);
            await dbContext.SaveChangesAsync();

            device.Temperature = 30.0;

            
            var result = await deviceRepository.Update(device);

            
            Assert.IsTrue(result);
            Assert.AreEqual(30.0, device.Temperature);
            Assert.AreEqual(device, dbContext.Set<Device>().Find(device.Id));
        }

        [TestMethod]
        public async Task RemoveDevice()
        {
            
            var device = new Device
            {
                Temperature = 25.5,
                Pin = "123456",
                DeviceStatusId = 1
            };
            dbContext.Set<Device>().Add(device);
            await dbContext.SaveChangesAsync();

            
            var result = await deviceRepository.Remove(device);

            
            Assert.IsTrue(result);
            Assert.IsFalse(dbContext.Set<Device>().Contains(device));
        }
    }
}