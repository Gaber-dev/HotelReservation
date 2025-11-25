using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Domain.Dtos.AccountDto
{
    public class UpdateProfileSettingsDto
    {
        public UpdateAddressInformationsDto? AddressInformations { get; set; }
        public UpdatePersonalInformationsDto? PersonalInformations { get; set; }
        public UpdateProfilepictureDto? Profilepicture { get; set; }
    }
}
