using Microsoft.EntityFrameworkCore;
using Vendas.Data;
using Vendas.Interfaces;
using Vendas.Services;
using Vendas.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<VendasDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    
builder.Services.AddSingleton<Vendas.MessageBus.IRabbitMQService, Vendas.MessageBus.RabbitMQService>();


builder.Services.AddOpenApi();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IVendaRepository, VendaRepository>();
builder.Services.AddScoped<IVendaService, VendaService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();
app.Run();

