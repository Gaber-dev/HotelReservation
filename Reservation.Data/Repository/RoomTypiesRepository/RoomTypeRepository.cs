using Microsoft.EntityFrameworkCore;
using Reservation.Data.Data;
using Reservation.Data.Model.Roomtype;
using Reservation.Data.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Data.Repository.RoomTypiesRepository
{
    public class RoomTypeRepository : GenericRepository<RoomType> , IRoomTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public RoomTypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<RoomType> FindRoombyName(string name)
        {
            var roomtype = await _context.RoomTypes.FirstOrDefaultAsync(t => t.Name == name);
            return roomtype;
        }
    }
}
