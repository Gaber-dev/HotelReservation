using Reservation.Data.Model.Amenities;
using Reservation.Data.Repository.AmenitiesRepository;
using Reservation.Domain.Dtos.AminityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Domain.Managers.AminityManager
{
    public class AminityService : IAminityService
    {
        private readonly IAminityRepository _aminityrepo;

        public AminityService(IAminityRepository aminityrepo)
        {
            _aminityrepo = aminityrepo;
        }



        public async Task AddAminity(AddAminityDto dtofromuser)
        {
            var aminitytorepo = new Amenity
            {
                Name = dtofromuser.Name,
                Description = dtofromuser.Description
            };

            await _aminityrepo.Add(aminitytorepo);
        }


    }
}
