using WarehouseInventoryManagement.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace WarehouseInventoryManagement.DataAccess.Repository
{
    public class DeviceRepository : GenericRepository<Device>, IDeviceRepository
    {
        public DeviceRepository(WarehouseInventoryManagementDBContext context) : base(context) { }

        public override async Task<List<Device>> GetAll(Expression<Func<Device, bool>> predicate)
        {
            return await  db.Set<Device>().Where(predicate).Include(s => s.DeviceStatus).ToListAsync();
        }

    }
}
