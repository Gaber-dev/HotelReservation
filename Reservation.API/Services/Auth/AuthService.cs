using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Reservation.Data.Model.Role;
using Reservation.Data.Model.User;
using Reservation.Domain.Dtos.AccountDto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Reservation.API.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _usermanager;
        private readonly RoleManager<AppRole> _rolemanager;
        private readonly IConfiguration _config;
        public AuthService(UserManager<AppUser> userManager , RoleManager<AppRole> roleManager , IConfiguration config)
        {
            _usermanager = userManager;
            _rolemanager = roleManager;
            _config = config;
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



        public async Task<LoginResponseDto> Login(LoginDto userdate)
        {
            var user = await _usermanager.FindByEmailAsync(userdate.Email);
            if (user == null)
                return null;
            var isPasswordExist = await _usermanager.CheckPasswordAsync(user, userdate.Password);
            if (!isPasswordExist) return null;

            var userroles = await _usermanager.GetRolesAsync(user);


            List<Claim> Claims = new List<Claim>();
            Claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            Claims.Add(new Claim(ClaimTypes.Name, user.FullName));
            foreach(var role in userroles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }
            Claims.Add(new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            // Credintial
            var mykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            SigningCredentials cred = new SigningCredentials(
                mykey,SecurityAlgorithms.HmacSha256
                );

            // Design Jwt
            JwtSecurityToken mytoken = new JwtSecurityToken(
                
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                expires: DateTime.Now.AddHours(1),
                claims: Claims,
                signingCredentials :cred
            );


            var finaltoken = new JwtSecurityTokenHandler().WriteToken(mytoken);

            var responseToUser = new LoginResponseDto()
            {
                Message = "Login Successfully",
                Token = finaltoken,
                TokenExpiration = DateTime.Now.AddHours(1)
            };

            return responseToUser;

        }



    }
}
