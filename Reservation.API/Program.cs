
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Reservation.Data.Data;
using Reservation.Data.Model.Role;
using Reservation.Data.Model.User;

namespace Reservation.API
{
    public class Program
    {
        public static void Main(string[] args)
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


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            builder.Services.AddScoped<IAuthService, AuthService>();




            
            var app = builder.Build();
            await SeedAdminData(app);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

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


