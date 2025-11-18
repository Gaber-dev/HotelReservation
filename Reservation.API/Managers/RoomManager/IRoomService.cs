using Reservation.Domain.Dtos.ImageDto;
using Reservation.Domain.Dtos.Pricesdto;
using Reservation.Domain.Dtos.RoomsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Domain.Managers.RoomManager
{
    public interface IRoomService
    {
        Task<string> AddRoom(CreateRoomDto roomdto);
       Task<IEnumerable<RoomDisplayDto>> GetFilteredAvailableRooms(RoomSearchFilterDto filterDto);
       Task<OneRoomDto> GetRoomDetailsWithReviewsAsync(int roomId);
        Task<CalculatePriceDto> CalculatePriceToRoom(PriceRequestCalculateDto dto);
    }
}
