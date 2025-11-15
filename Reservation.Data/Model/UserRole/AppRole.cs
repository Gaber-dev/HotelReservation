using Microsoft.AspNetCore.Identity;
using Reservation.Data.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Data.Model.Role
{
    // Role 
    public class AppRole : IdentityRole
    {
        public string? Description { get; set; }
    }
}
