using STPP_Project.Data;
using STPP_Project.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<ProjectDbContext>();
builder.Services.AddTransient<ICitiesRepository, CitiesRepository>();
builder.Services.AddTransient<IAccommodationAdsRepository, AccommodationAdsRepository>();
builder.Services.AddTransient<IReservationsRepository, ReservationsRepository>();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();
