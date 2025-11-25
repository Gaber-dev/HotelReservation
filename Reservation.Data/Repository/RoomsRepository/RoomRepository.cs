using Microsoft.EntityFrameworkCore;
using Reservation.Data.Data;
using Reservation.Data.Model.Rooms;
using Reservation.Data.Repository.GenericRepository;
using Reservation.Domain.Dtos.RoomsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Data.Repository.RoomsRepository
{
    public class RoomRepository : GenericRepository<Room> , IRoomRepository
    {
        private readonly ApplicationDbContext _context;

        public RoomRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }


        public async Task<IEnumerable<Room>> SearchAvailableRooms(RoomSearchFilterDto dto)
        {
            var query = _context.Rooms
                .Include(r => r.RoomType)
                .Include(r => r.RoomImages)
                .Include(r => r.Amenities).ThenInclude(ra => ra.Amenity)
                .Include(r => r.Reservations).ThenInclude(res => res.ReservationReview)
                .AsQueryable();

            var checkIn = dto.CheckInDate?.Date ?? DateTime.Today;
            var checkOut = dto.CheckOutDate?.Date ?? DateTime.Today.AddDays(1);

            query = query.Where(room => !room.Reservations.Any(res =>
                res.CheckinDate < checkOut && res.CheckoutDate >= checkIn));

            if (dto.NumberOfGuests > 0)
                query = query.Where(r => r.RoomType.MaxOccupancy >= dto.NumberOfGuests.Value);

            if (dto.MinPrice.HasValue)
                query = query.Where(r => r.PricePerroom >= dto.MinPrice.Value);

            if (dto.MaxPrice.HasValue)
                query = query.Where(r => r.PricePerroom <= dto.MaxPrice.Value);

            if (dto.RoomTypes != null && dto.RoomTypes.Any())
                query = query.Where(r => dto.RoomTypes.Contains(r.RoomType.Name));

            if (dto.Amenities != null && dto.Amenities.Any(x => !string.IsNullOrWhiteSpace(x)))
            {
                var requestedAmenities = dto.Amenities
                    .Where(a => !string.IsNullOrWhiteSpace(a))
                    .Select(a => a.Trim())
                    .ToList();

                query = query.Where(room =>
                    requestedAmenities.All(required =>
                        room.Amenities.Any(ra =>
                            ra.Amenity.Name.Trim() == required)));
            }

            return await query.ToListAsync();
        }


        public async Task<Room?> GetRoomDetails(int roomId)
        {
            return await _context.Rooms.Include(r => r.RoomType)
                          .Include(r => r.RoomImages)
                          .Include(r => r.Amenities).ThenInclude(r => r.Amenity)
                          .Include(r => r.Reservations).ThenInclude(r => r.ReservationReview)
                          .AsSplitQuery()
                          .FirstOrDefaultAsync();
         }


    }
}
