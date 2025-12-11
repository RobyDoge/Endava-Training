using AirportTool.Application.Abstractions;
using AirportTool.Infrastructure.Configurations;
using AirportTool.Infrastructure.Context;
using AirportTool.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("AirportToolDbConnectionString");
builder.Services.AddDbContext<AirlineBookingContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddAutoMapper(cfg => { }, typeof(EfDomainMapper));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IFlightScheduleRepository, FlightScheduleRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();