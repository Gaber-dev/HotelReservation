using Reservation.Data.Data;
using Reservation.Data.Model.RoomImages;
using Reservation.Data.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Data.Repository.RoomImagesRepository
{
    public class RoomImageRepository : GenericRepository<RoomImage> , IRoomImageRepository
    {
        private readonly ApplicationDbContext _context;

        public RoomImageRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }
    }
}
