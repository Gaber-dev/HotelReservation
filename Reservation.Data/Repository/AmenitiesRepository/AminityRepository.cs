using Microsoft.EntityFrameworkCore;
using Reservation.Data.Data;
using Reservation.Data.Model.Amenities;
using Reservation.Data.Model.Roomtype;
using Reservation.Data.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Data.Repository.AmenitiesRepository
{
    public class AminityRepository : GenericRepository<Amenity> , IAminityRepository
    {
        private readonly ApplicationDbContext _context;

        public AminityRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<Amenity> FindAmenitybyName(string name)
        {
            var Aminity = await _context.Amenity.FirstOrDefaultAsync(a => a.Name == name);
            return Aminity;
        }
    }
}
