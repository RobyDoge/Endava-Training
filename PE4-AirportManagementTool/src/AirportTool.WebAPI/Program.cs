using AirportTool.Application.Abstractions;
using AirportTool.Application.Services;
using AirportTool.Infrastructure.Configurations;
using AirportTool.Infrastructure.Context;
using AirportTool.Infrastructure.Repositories;
using AirportTool.WebAPI.Configurations;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using AirportTool.Application.Validators;

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

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<DomainDtoProfile>();
    cfg.AddProfile<EfDomainMapper>();
    cfg.AddProfile<RequestRecordProfile>();
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IFlightScheduleRepository, FlightScheduleRepository>();
builder.Services.AddScoped<IFlightRepository, FlightRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();

builder.Services.AddScoped<FlightScheduleService>();
builder.Services.AddScoped<FlightService>();
builder.Services.AddScoped<TicketsService>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateFlightValidator>();

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