using Ayniyat.Dal;
using Ayniyat.Dal.Abstract;
using Ayniyat.Dal.Concrete;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IDaireDal, DaireDal>();



// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DefaultDbContext>(options =>
  options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
            o =>
            {
                o.MigrationsHistoryTable("_mig_history", "ayniyat");
            }));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
