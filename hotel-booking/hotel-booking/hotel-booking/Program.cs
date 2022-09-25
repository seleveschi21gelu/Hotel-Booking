using hotel_booking.BLL;
using hotel_booking.DAL;
using hotel_booking.IBLL;
using hotel_booking.IRepository;
using hotel_booking.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Api Key Auth", Version = "v1" });
    c.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "ApiKey must appear in header",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Name = "ApiKey",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Scheme = "ApiKeyScheme"
    });

    var key = new OpenApiSecurityScheme()
    {
        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "ApiKey" },
        In = ParameterLocation.Header
    };
    var requirement = new OpenApiSecurityRequirement { { key, new List<string>() } };
    c.AddSecurityRequirement(requirement);
});

//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "dotnetClaimAuthorization", Version = "v1" });
//    c.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
//    {
//        Description = "Please insert token",
//        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
//        Name = "Authorization",
//        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
//        Scheme = "bearer",
//        BearerFormat = "JWT"
//    });

//    c.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        { new OpenApiSecurityScheme
//        {
//            Reference = new OpenApiReference{
//                Type = ReferenceType.SecurityScheme,
//                Id = "Bearer"
//            }
//        }, new string[]{}
//      }
//    });
//});

string connString = builder.Configuration.GetConnectionString("DefaultConnectionString");
builder.Services.AddDbContext<AppDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// add dependency injection for repositories and business logic layers

builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IHotelBLL, HotelBLL>();

builder.Services.AddScoped<IPicturesRepository, PicturesRepository>();

builder.Services.AddScoped<IHotelDetailsRepository, HotelDetailsRepository>();
builder.Services.AddScoped<IHotelDetailsBLL, HotelDetailsBLL>();

builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBookingBLL, BookingBLL>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();
app.UseWebSockets();

//now handle other requests (default, static files, mvc actions, ...)
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
