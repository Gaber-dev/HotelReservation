using Reservation.Domain.Dtos.ReservationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Domain.Dtos.AccountDto
{
    public class ProfileSettingsDto
    {
        public ProfilepictureDto Profilepicture { get; set; }
        public PersonalInformationsDto PersonalInformations { get; set; }
        public AddressinformationsDto AddressInformations { get; set; }
    }
}
