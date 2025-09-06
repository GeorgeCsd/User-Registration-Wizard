using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RegistWizard.Api;          
using RegistWizard.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;

}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("ng", p => p.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod());
});
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var database = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await database.Database.MigrateAsync();
    if (!database.Industries.Any())
    {
        database.Industries.AddRange
        (new Industry {Name = "Finance & Banking"},
    new Industry {Name = "Insurance"},
    new Industry {Name = "Retail"},
    new Industry {Name = "E-commerce"},
    new Industry {Name = "Telecommunications"},
    new Industry {Name = "Media & Entertainment"},
    new Industry {Name = "Automotive"},
    new Industry {Name = "Aerospace & Defense"},
    new Industry {Name = "Agriculture"},
    new Industry {Name = "Food & Beverage"},
    new Industry {Name = "Logistics & Supply Chain"},
    new Industry {Name = "Transportation"},
    new Industry {Name = "Travel & Hospitality"},
    new Industry {Name = "Real Estate"},
    new Industry {Name = "Construction"},
    new Industry {Name = "Energy"},
    new Industry {Name = "Oil & Gas"},
    new Industry {Name = "Renewable Energy"},
    new Industry {Name = "Utilities"},
    new Industry {Name = "Chemicals"},
    new Industry {Name = "Pharmaceuticals"},
    new Industry {Name = "Biotechnology"},
    new Industry {Name = "Medical Devices"},
    new Industry {Name = "Consumer Electronics"},
    new Industry {Name = "Hardware & IoT"},
    new Industry {Name = "Cybersecurity"},
    new Industry {Name = "Cloud & SaaS"},
    new Industry {Name = "Data & Analytics"},
    new Industry {Name = "Artificial Intelligence"},
    new Industry {Name = "Professional Services"},
    new Industry {Name = "Consulting"});
        await database.SaveChangesAsync();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("ng");
app.MapControllers();
app.Run();
