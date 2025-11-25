using Reservation.Data.Model.RoomAmenities;
using Reservation.Data.Model.RoomImages;
using Reservation.Data.Model.Rooms;
using Reservation.Data.Repository.AmenitiesRepository;
using Reservation.Data.Repository.RoomAminitiesRepository;
using Reservation.Data.Repository.RoomImagesRepository;
using Reservation.Data.Repository.RoomsRepository;
using Reservation.Data.Repository.RoomTypiesRepository;
using Reservation.Domain.Dtos.ImageDto;
using Reservation.Domain.Dtos.Pricesdto;
using Reservation.Domain.Dtos.Reviews;
using Reservation.Domain.Dtos.RoomsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Domain.Managers.RoomManager
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IAminityRepository _aminityrepo;
        private readonly IRoomTypeRepository _roomType;
        private readonly IRoomImageRepository _roomimage;
        private readonly IRoomAminityRepository _roomaminrepo;

        public RoomService(IRoomRepository roomRepository , IAminityRepository aminityrepo , IRoomTypeRepository roomType , IRoomImageRepository roomimage , IRoomAminityRepository roomaminrepo)
        {
            _roomRepository = roomRepository;
            _aminityrepo = aminityrepo;
            _roomType = roomType;
            _roomimage = roomimage;
            _roomaminrepo = roomaminrepo;
        }

        
        public async Task<string> AddRoom(CreateRoomDto roomdto)
        {
            var roomType = await _roomType.FindRoombyName(roomdto.RoomName);
            if (roomType == null)
                throw new Exception($"Room type {roomdto.RoomName} is not found");

            var room = new Room
            {
                Number = roomdto.RoomNumber,
                FloorNumber = roomdto.FloorNumber,
                Description = roomdto.Description,
                PricePerroom = roomdto.PricePerRoom,
                Status = roomdto.Status,
                Size = roomdto.RoomSize,
                View = roomdto.View,
                Smooking = roomdto.Smooking,
                RoomTypeId = roomType.Id
            };

            
            await _roomRepository.Add(room); 

            
            foreach (var amin in roomdto.Amenities)
            {
                var amenity = await _aminityrepo.FindAmenitybyName(amin);
                if (amenity != null)
                {
                    var RoomAmenity = new RoomAmenity
                    {
                        RoomId = room.Id,     
                        AmenityId = amenity.Id
                    };

                    await _roomaminrepo.Add(RoomAmenity);
                }
            }

           
            foreach (var img in roomdto.Images)
            {
                var RoomImageToDB = new RoomImage
                {
                    ImageUrl = img,
                    RoomId = room.Id   
                };

                await _roomimage.Add(RoomImageToDB);
            }

            return "Room Added Successfully";
        }

        public async Task<IEnumerable<RoomDisplayDto>> GetFilteredAvailableRooms(RoomSearchFilterDto filterDto)
        {
            var rooms = await _roomRepository.SearchAvailableRooms(filterDto);

            var roomToUsers = rooms.Select(r => new RoomDisplayDto
            {
                Id = r.Id,
                RoomTypeName = r.RoomType.Name,
                Name = $"{r.View} {r.RoomType.Name}",
                PricePerNight = r.PricePerroom,
                BedType = r.RoomType.BedType,
                MaxOccupancy = r.RoomType.MaxOccupancy,
                Size = r.Size,

                Rating = r.Reservations
                    .Where(res => res.ReservationReview != null && res.ReservationReview.Rating.HasValue)
                    .Select(res => res.ReservationReview.Rating.Value)
                    .DefaultIfEmpty(0)
                    .Average(),

                ReviewCount = r.Reservations.Count(res => res.ReservationReview != null),
                MainImageUrl = r.RoomImages
                    .OrderBy(ri => ri.Id)
                    .Select(ri => ri.ImageUrl)
                    .FirstOrDefault()
                    ?? string.Empty,   

                
                Amenities = r.Amenities
                    .Select(ra => ra.Amenity.Name)
                    .ToList()
            }).ToList();

            return roomToUsers;
        }



        
        public async Task<OneRoomDto> GetRoomDetailsWithReviewsAsync(int roomId)
        {
            var room = await _roomRepository.GetRoomDetails(roomId);

            if (room == null)
                throw new KeyNotFoundException("Room not found");

            
            var validReservations = room.Reservations
                .Where(r =>
                    r.Guest != null &&
                    r.ReservationReview != null &&
                    (r.status == "Completed" || r.status == "CheckedOut") &&
                    r.CheckoutDate.Date < DateTime.Today.Date
                )
                .OrderByDescending(r => r.ReservationReview.CreatedAt)
                .ToList();

            var avgRating = validReservations.Any()
        ? Math.Round(validReservations
            .Select(r => (double)r.ReservationReview.Rating)
            .Average(), 1)
        : 0.0;

            return new OneRoomDto
            {
                Id = room.Id,
                Name = $"{room.View} {room.RoomType.Name}".Trim(),
                Description = room.Description ?? "Luxurious room with stunning views.",
                PricePerNight = room.PricePerroom,
                Size = room.Size,
                View = room.View,
                BedType = room.RoomType.BedType,
                MaxOccupancy = room.RoomType.MaxOccupancy,
                Smoking = !string.IsNullOrWhiteSpace(room.Smooking) &&
                  (room.Smooking.Trim().ToLower() == "yes" ||
                   room.Smooking.Trim().ToLower() == "true" ||
                   room.Smooking.Trim() == "1")
                      ? "Smoking"
                      : "Non-Smoking",

                Rating = avgRating,
                ReviewCount = validReservations.Count,

                Images = room.RoomImages
                    .OrderBy(ri => ri.Id)
                    .Select(ri => ri.ImageUrl)
                    .Take(10)
                    .ToList(),

                Amenities = room.Amenities
                    .Select(a => a.Amenity.Name)
                    .Distinct()
                    .ToList(),

                Reviews = validReservations
                    .Take(10)
                    .Select(r => new ReservationReviewsDto
                    {
                        GuestName = r.Guest.FullName?.Trim() ?? "Guest",
                        Rating = r.ReservationReview.Rating,
                        Comment = r.ReservationReview.Comment ?? "",
                        ReviewDate = r.ReservationReview.CreatedAt.Date
                    })
                    .ToList()
            };
        }

        
        private string GetInitial(string? name)
        {
            if (string.IsNullOrWhiteSpace(name)) return "G";
            var parts = name.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return parts.Length > 0 ? parts[0][0].ToString().ToUpper() : "G";
        }




        public async Task<CalculatePriceDto> CalculatePriceToRoom(PriceRequestCalculateDto dto)
        {
            var roomfromDb = await _roomRepository.GetRoomDetails(dto.RoomId);
            if (roomfromDb == null)
                throw new Exception("Room not found");

            var nights = (dto.CheckOutDate - dto.CheckInDate).Days;
            if (nights <= 0)
                throw new Exception("Invalid dates");
            var subtotals = roomfromDb.PricePerroom * nights;

            return new CalculatePriceDto
            {
                PricePerNight = roomfromDb.PricePerroom,
                subtotals =(int) subtotals,
                TaxesAndFees = 40.0,
                ServiceCharge = 20.0,
                NumberOfGuests = dto.Guests,
                TotalAmount = subtotals + 40.0 + 20.0
            };
        }

    }    
}

