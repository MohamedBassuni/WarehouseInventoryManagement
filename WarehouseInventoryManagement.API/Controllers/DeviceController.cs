using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WarehouseInventoryManagement.Business.Models;
using WarehouseInventoryManagement.Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;
using WarehouseInventoryManagement.DataAccess.Entities;

namespace WarehouseInventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private IDeviceService deviceService;
        public DeviceController(IDeviceService deviceService)
        {
            this.deviceService = deviceService;
        }

        [HttpPost("add")]
        public async Task<ActionResult> Add(DeviceCreationDTO deviceDTO)
        {

            var device = await deviceService.Add(deviceDTO);
            return Ok(new
            {
                data = device
            });
        }
        [HttpGet("get-active-devices")]
        public async Task<ActionResult> GetActiveDevices()
        {

            var device = await deviceService.GetAllActiveDevices();

            return Ok(new
            {
                data = device
            });
        }

        [HttpPut("update/{id:int}")]
        public async Task<ActionResult> Update(int id, DeviceUpdatingDTO deviceDTO)
        {
            await deviceService.Update(id, deviceDTO);

            return Ok(new
            {
                Message = "Device Updated"
            });
        }

        [HttpDelete("remove/{id:int}")]

        public async Task<ActionResult> Remove(int id)
        {
            await deviceService.Remove(id);
            return Ok(new
            {
                Message = "Device Removed"
            });
        }
    }
}
