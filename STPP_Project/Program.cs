using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using STPP_Project.Auth;
using STPP_Project.Auth.Model;
using STPP_Project.Data;
using STPP_Project.Data.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using static System.Formats.Asn1.AsnWriter;

var builder = WebApplication.CreateBuilder(args);

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddControllers();

builder.Services.AddIdentity<ProjectRestUser, IdentityRole>()
    .AddEntityFrameworkStores<ProjectDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters.ValidAudience = builder.Configuration["JWT:ValidAudience"];
        options.TokenValidationParameters.ValidIssuer = builder.Configuration["JWT:ValidIssuer"];
        options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]));
    });

builder.Services.AddDbContext<ProjectDbContext>();
builder.Services.AddTransient<ICitiesRepository, CitiesRepository>();
builder.Services.AddTransient<IAccommodationAdsRepository, AccommodationAdsRepository>();
builder.Services.AddTransient<IReservationsRepository, ReservationsRepository>();
builder.Services.AddTransient<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<AuthDbSeeder>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(PolicyNames.ResourceOwner, policy => policy.Requirements.Add(new ResourceOwnerRequirement()));
});

builder.Services.AddSingleton<IAuthorizationHandler, ResourceOwnerAuthorizationHandler>();

builder.Services.AddCors(cr => {
    cr.AddPolicy("allowAll", cp =>
    {
        //cp.WithOrigins("http://localhost:3000", "https://localhost:7108");
        cp.AllowAnyOrigin();
        cp.AllowAnyMethod();
        cp.AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseRouting();
app.UseCors("allowAll");
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

//var dbSeeder = scope.ServiceProvider.GetRequiredService<AuthDbSeeder>();
var dbSeeder = app.Services.CreateScope().ServiceProvider.GetRequiredService<AuthDbSeeder>();
await dbSeeder.SeedAsync();

app.Run();
