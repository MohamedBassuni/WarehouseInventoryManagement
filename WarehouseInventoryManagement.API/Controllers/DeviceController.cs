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

namespace WarehouseInventoryManagement.API.Controllers
{
    [Route("api/device")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private IDeviceService deviceService;
        public DeviceController(IDeviceService deviceService)
        {
            this.deviceService = deviceService;
        }

        [HttpPost("add")]
        public async Task<ActionResult> Add(DeviceDTO deviceDTO)
        {
            
            var device = await deviceService.Add(deviceDTO);

            return Ok(new
            {
                data = device
            });
        }

        [HttpPost("update")]
        public async Task<ActionResult> update(DeviceDTO deviceDTO)
        {
            var isUpdated = await deviceService.Update(deviceDTO);
            if (isUpdated)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPost("remove")]
        public async Task<ActionResult> remove(int deviceId)
        {
            var isRemoved = await deviceService.Remove(deviceId);
            if (isRemoved)
                return Ok();
            else
                return BadRequest();
        }
    }
}
