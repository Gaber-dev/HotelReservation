
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Reservation.API.Managers.Paymob;
using Reservation.API.Managers.ReservationManager;
using Reservation.API.Services.Auth;
using Reservation.API.Services.Email;
using Reservation.Data.Data;
using Reservation.Data.Model.Role;
using Reservation.Data.Model.User;
using Reservation.Data.Repository.AmenitiesRepository;
using Reservation.Data.Repository.RoomAminitiesRepository;
using Reservation.Data.Repository.RoomImagesRepository;
using Reservation.Data.Repository.RoomsRepository;
using Reservation.Data.Repository.RoomTypiesRepository;
using Reservation.Domain.Managers.AminityManager;
using Reservation.Domain.Managers.RoomManager;
using Reservation.Domain.Managers.RoomTypeManager;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Configure Database and Identity database else
            builder.Services.AddDbContext<ApplicationDbContext>(
                options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection"));
                }
                );

            builder.Services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();



            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],

                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["Jwt:Audience"],

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });


            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Reservation API",
                    Version = "v1"
                });

                // Add JWT Authentication to Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.\r\n\r\n" +
                                  "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                                  "Example: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
            });


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IAminityService, AminityService>();
            builder.Services.AddScoped<IAminityRepository, AminityRepository>();
            builder.Services.AddScoped<IRoomAminityRepository, RoomAminityRepository>();
            builder.Services.AddScoped<IRoomImageRepository, RoomImageRepository>();
            builder.Services.AddScoped<IRoomService , RoomService>();
            builder.Services.AddScoped<IRoomRepository ,  RoomRepository>();
            builder.Services.AddScoped<IRoomTypeRepository ,  RoomTypeRepository>();
            builder.Services.AddScoped<IRoomTypeService, RoomTypeService>();
            builder.Services.AddScoped<IReservationService , ReservationService>();
            builder.Services.AddScoped<IAddressesRepository , AddressesRepository>();
            builder.Services.AddScoped<IReviewService , ReviewService>();
            builder.Services.AddScoped<IHotelReviewRepository , HotelReviewRepository>();
            builder.Services.AddScoped<IReservationReviewRepository , ReservationReviewRepository>();
            builder.Services.AddHttpClient<IPaymobService, PaymobService>();

            var app = builder.Build();

            await SeedAdminData(app);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();


            // Seeding Admin Data
            async Task SeedAdminData(IApplicationBuilder app)
            {
                using var scope = app.ApplicationServices.CreateScope();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

                string adminEmail = "adminadmin@hotel.com";
                string adminPassword = "Admin@12345";
                string roleName = "Admin";

                if(await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new AppRole
                    {
                        Name = roleName,
                        Description = "Admin Role that have upper hand"
                    });
                }


                var isAdminExist = userManager.FindByEmailAsync(adminEmail);
                if (isAdminExist == null)
                {
                    var newadmin = new AppUser
                    {
                        FullName = adminEmail,
                        Email = adminEmail,
                        UserName = adminEmail,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(newadmin,adminPassword);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(newadmin,roleName);
                    }

                }

            }


        }
    }
}

