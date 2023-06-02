using AutoMapper;
using WarehouseInventoryManagement.API;
using WarehouseInventoryManagement.Business.Services;
using WarehouseInventoryManagement.DataAccess.Repository;
using WarehouseInventoryManagement.DataAccess;
using WarehouseInventoryManagement.Business.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddControllers();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IDSService, DSService>();
builder.Services.AddDbContext<WarehouseInventoryManagementDBContext>(
 options => options.UseSqlServer("name=ConnectionStrings:WarehouseInventoryManagementDb"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigureExceptionHandler();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();

