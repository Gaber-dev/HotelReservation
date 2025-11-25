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
    public interface IRoomRepository : IGenericRepository<Room>
    {
       Task<IEnumerable<Room>> SearchAvailableRooms(RoomSearchFilterDto dto);
       Task<Room?> GetRoomDetails(int roomId);
    }
}
