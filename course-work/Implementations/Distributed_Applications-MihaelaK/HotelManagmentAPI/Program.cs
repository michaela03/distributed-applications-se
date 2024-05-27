using HotelManagmentAPI.Data;
using HotelManagmentAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using HotelManagmentAPI.Helper;
using HotelManagmentAPI.Repository;
using HotelManagmentAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfiles)); // Register AutoMapper
builder.Services.AddScoped<IHotelRepository, RoomRepository>(); // Register RoomRepository
builder.Services.AddScoped<IClientsRepository, ClientRepository>(); // Register ClientRepository
builder.Services.AddScoped<IReviewRepository, ReviewRepository>(); // Register ReservationRepository
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();// Register ReviewRepository
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelManagementAPI v1"));
}
app.UseMiddleware<ApiKeyMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
