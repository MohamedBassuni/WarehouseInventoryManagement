using AutoMapper;
using WarehouseInventoryManagement.Business.Models;
using WarehouseInventoryManagement.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseInventoryManagement.Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Device, DeviceDTO>()
                .ForMember(s => s.DeviceStatus, des => des.MapFrom(s => s.DeviceStatus.Name)).ReverseMap();

        }
    }
}
