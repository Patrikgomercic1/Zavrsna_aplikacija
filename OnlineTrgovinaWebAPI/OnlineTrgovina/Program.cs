using Microsoft.EntityFrameworkCore;
using OnlineTrgovina.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dokumentacija
builder.Services.AddSwaggerGen(sgo => { 
    var o = new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "Online Trgovina API",
        Version = "v1",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
        {
            Email = "tjakopec@gmail.com",
            Name = "Patrik Gomerèiæ"
        },
        Description = "Ovo je dokumentacija za Online Trgovina API",
        //License = new Microsoft.OpenApi.Models.OpenApiLicense()
        //{
        //    Name = "Edukacijska licenca"
        //}
    };
    sgo.SwaggerDoc("v1", o);
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    sgo.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

});


builder.Services.AddDbContext<OTContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString(name: "OTContext")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(opcije =>
    {
        opcije.SerializeAsV2 = true;
    });
    app.UseSwaggerUI(opcije =>
    {
        opcije.ConfigObject.AdditionalItems.Add("requestSnippetsEnabled", true);
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseStaticFiles();

app.Run();
