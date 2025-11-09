using Microsoft.AspNetCore.Identity;
using Reservation.Data.Model.Role;
using Reservation.Data.Model.User;
using Reservation.Domain.Dtos.AccountDto;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Reservation.API.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _usermanager;
        private readonly RoleManager<AppRole> _rolemanager;
        public AuthService(UserManager<AppUser> userManager , RoleManager<AppRole> roleManager)
        {
            _usermanager = userManager;
            _rolemanager = roleManager;
        }

        public async Task<string> RegisterClientAsync(RegisterDto clientdto)
        {
            return await RegisterUserAsync(clientdto, "Client", "Authenticated person in my website");
        }

        public async Task<string> RegisterReceptionist(RegisterDto ReceptionistDto)
        {
            return await RegisterUserAsync(ReceptionistDto, "Receptionist", "Person that receive requests from clients");
        }





        private async Task<string> RegisterUserAsync(RegisterDto dto , string roleName , string roleDescription)
        {
            var isRoleExist = await _rolemanager.RoleExistsAsync(roleName);   // هتاكد ان الرول موجود في الاول
            if (!isRoleExist)
            {
               await _rolemanager.CreateAsync(new AppRole                     // لو مش موجود هعمل اوبجكت واضيف رول جديد
                {
                    Name = roleName,
                    Description = roleDescription
                });
            }

            //  هتاكد ان اليوزر موجود ولا لا بالايميل
            var isUserExist = await _usermanager.FindByEmailAsync(dto.EmailAddress);
            if (isUserExist != null)
                return "البريد الالكتروني مستخدم بالفعل";

            // Map dto to AppUser
            var user = new AppUser
            {
                FullName = dto.FullName,
                Email = dto.EmailAddress,
                PhoneNumber = dto.PhoneNumber,
                UserName = dto.EmailAddress
            };


            
            // Create User
            var result =  await _usermanager.CreateAsync(user,dto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(" | ", result.Errors.Select(e => e.Description));
                return $"فشل في تسجيل مستخدم جديد {errors}";
            }
            else
            {
                // Assign role to user
                await _usermanager.AddToRoleAsync(user, roleName);
                return "تم إنشاء الحساب بنجاح";
            }
        }



    }
}
