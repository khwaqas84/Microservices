using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OrderService.Consumers;
using OrderService.Database;
using OrderService.Services.Impelmentations;
using OrderService.Services.Interfaces;
using Shared.Database;
using Shared.Messages.Commands;
using Shared.StateMachine;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});
builder.Services.AddScoped<IOrderDataAccess, OrderDataAccess>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
builder.Services.AddMassTransit(config =>
{
    config.AddRequestClient<IOrderStart>();

    config.AddConsumer<OrderStartConsumer>();
    config.AddConsumer<OrderAcceptedConsumer>();
    config.AddConsumer<OrderCancelledConsumer>();

    //state machine
    config.AddSagaStateMachine<OrderStateMachine, OrderState>()
                      .EntityFrameworkRepository(r =>
                      {
                          r.ConcurrencyMode = ConcurrencyMode.Pessimistic;
                          r.AddDbContext<DbContext, OrderStateDbContext>((provider, options) =>
                          {
                              options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
                          });
                      });

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.ConfigureEndpoints(ctx);
        var uri = new Uri(builder.Configuration["ServiceBus:Uri"]);
        cfg.Host(uri, host =>
        {
            host.Username(builder.Configuration["ServiceBus:Username"]);
            host.Password(builder.Configuration["ServiceBus:Password"]);
        });
    });
});

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
