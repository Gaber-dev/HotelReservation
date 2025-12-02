using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Domain.Dtos.AccountDto
{
    public class LoginDto
    {
        [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
        [EmailAddress(ErrorMessage = "صيغة البريد الإلكتروني غير صحيحة")]
        public string Email { get; set; }

       
        
        [Required(ErrorMessage = "كلمة المرور مطلوبة")]
        public string Password { get; set; }
    }
}
