using Reservation.Data.Model.Roomtype;
using Reservation.Data.Repository.RoomTypiesRepository;
using Reservation.Domain.Dtos.ImageDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Domain.Managers.RoomTypeManager
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository _repo;

        public RoomTypeService(IRoomTypeRepository repo)
        {
            _repo = repo;
        }

        public async Task Add(AddRoomType dto)
        {
            var RoomType = new RoomType
            {
                Name = dto.Name,
                BasePrice = dto.BasePrice,
                MaxOccupancy = dto.MaxOccupancy,
                BedType = dto.BedType
            };
            await _repo.Add(RoomType);
        }


    }
}
