
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Reservation.API.Services.Auth;
using Reservation.API.Services.Email;
using Reservation.Data.Data;
using Reservation.Data.Model.Role;
using Reservation.Data.Model.User;
using System.Text;
using System.Threading.Tasks;
using Reservation.Domain.Interfaces;
using Reservation.Infrastructure.Services;

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


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IEmailService, EmailService>();

            builder.Services.AddScoped<IHotelService, HotelService>();



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

                string adminEmail = "....";
                string adminPassword = "...";
                string roleName = "..";

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
