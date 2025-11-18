using Reservation.Data.Data;
using Reservation.Data.Model.RoomAmenities;
using Reservation.Data.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Data.Repository.RoomAminitiesRepository
{
    public class RoomAminityRepository : GenericRepository<RoomAmenity> , IRoomAminityRepository
    {
        private readonly ApplicationDbContext _context;

        public RoomAminityRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        
    }
}
