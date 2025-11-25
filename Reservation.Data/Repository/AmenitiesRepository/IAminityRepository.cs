using Reservation.Data.Model.Amenities;
using Reservation.Data.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Data.Repository.AmenitiesRepository
{
    public interface IAminityRepository : IGenericRepository<Amenity>
    {
        Task<Amenity> FindAmenitybyName(string name);
    }
}
