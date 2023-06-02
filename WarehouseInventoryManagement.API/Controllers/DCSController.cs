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
    [Route("api/[controller]")]
    [ApiController]
    public class DCSController : ControllerBase
    {
        private IDSService dsService;
        public DCSController(IDSService dsService)
        {
            this.dsService = dsService;
        }

        [HttpPost("Configure/{id:int}")]
        public async Task<ActionResult> Configure(int id, DeviceConfigurationDTO deviceDTO)
        {

            var device = await dsService.ConfigureDevice(id, deviceDTO);

            return Ok(new
            {
                data = "Device Configured"
            });
        }


    }
}
