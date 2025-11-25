using Reservation.Domain.Dtos.ImageDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Domain.Managers.RoomTypeManager
{
    public interface IRoomTypeService
    {
        Task Add(AddRoomType dto);
    }
}
