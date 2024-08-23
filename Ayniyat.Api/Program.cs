using Ayniyat.Dal;
using Ayniyat.Dal.Abstract;
using Ayniyat.Dal.Concrete;
using Ayniyat.Models.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IDaireDal, DaireDal>();
builder.Services.AddTransient<IKullaniciDal, KullaniciDal>();
builder.Services.AddTransient<IRolDal, RolDal>();
builder.Services.AddTransient<ISubeDal, SubeDal>();
builder.Services.AddTransient<IZimmetDal, ZimmetDal>();
builder.Services.AddTransient<IZimmetLogDal, ZimmetLogDal>();



var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options =>
   {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidIssuer = tokenOptions.Issuer,
           ValidAudience = tokenOptions.Audience,
           ValidateIssuerSigningKey = true,
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
       };
   });


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

app.UseCors(builder =>
        builder
        //.WithOrigins("http://localhost:8087")
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
