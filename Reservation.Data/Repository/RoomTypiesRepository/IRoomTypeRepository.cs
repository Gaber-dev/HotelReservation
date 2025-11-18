using Reservation.Data.Model.Roomtype;
using Reservation.Data.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Data.Repository.RoomTypiesRepository
{
    public interface IRoomTypeRepository : IGenericRepository<RoomType>
    {
        Task<RoomType> FindRoombyName(string name);
    }
}
