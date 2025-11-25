using Microsoft.AspNetCore.Identity;
using Reservation.Data.Model.User;
using Reservation.Data.Model.UserAddress;
using Reservation.Data.Repository.AddressRepository;
using Reservation.Data.Repository.ReservationRepository;
using Reservation.Data.Repository.RoomsRepository;
using Reservation.Data.Repository.RoomTypiesRepository;
using Reservation.Domain.Dtos.AccountDto;
using Reservation.Domain.Dtos.ReservationDto;

namespace Reservation.API.Managers.AccountManager
{
    public class DashbordService  : IDashbordService
    {
        private readonly UserManager<AppUser> _user;
        private readonly IRoomRepository _roomrepo;
        private readonly IRoomTypeRepository _roomtyperepo;
        private readonly IReservationsRepository _reservationsrepo;
        private readonly IAddressesRepository _addressrepo;

        public DashbordService(UserManager<AppUser> user , IRoomRepository roomrepo , IRoomTypeRepository roomtyperepo , IReservationsRepository reservationsrepo , IAddressesRepository addressrepo)
        {
            _user = user;
            _roomrepo = roomrepo;
            _roomtyperepo = roomtyperepo;
            _reservationsrepo = reservationsrepo;
            _addressrepo = addressrepo;
        }


        public async Task<DashbordDto> GetDashbordDetails(string userId)
        {
            var user = await _user.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("Please make an account to continue");

            var imageUrl = user.ProfileImage;
            var fullname = user.FullName;
            var email = user.Email;
            var totalReservations = await _reservationsrepo.GetTotalReservatioCount(userId);

            var totalSpent = await _reservationsrepo.GetTotalReservatioMoneyspent(userId);

            var currentReservations = await _reservationsrepo.GetCurrentReservations(userId);

            var reservationsHistories = await _reservationsrepo.GetReservationsHistory(userId);
            var welcomestring = $"Welcome back, {user.FullName}!\r\nManage your reservations and profile";
            return new DashbordDto
            {
                imageUrl = imageUrl,
                FullName = fullname,
                Email = email,
                TotalReservation = totalReservations,
                TotalSpent = totalSpent,
                CurrentReservations = currentReservations,
                GetReservationHistory = reservationsHistories,
                WelcomeString =  welcomestring
            };
        }


        public async Task<List<MyReservationsDto>> MyReservations(string userId)
        {
            var allreservations = await _reservationsrepo.MyReservationsAll(userId);
            return allreservations;
        }



        public async Task<ProfileSettingsDto> ProfileSettings(string userId)
        {
            var user = await _user.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");


            var address = await _addressrepo.FindAddressById(userId);

            var addressinfo = new AddressinformationsDto
            {
                StreetAddress = address?.Street,
                City = address?.City,
                country = address?.Country,
                ZipCode = address?.ZipCode ?? 0  
            };

            var imageinfo = new ProfilepictureDto
            {
                ImageUrl = user.ProfileImage
            };
            var personalinfo = new PersonalInformationsDto
            {
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                dateOFBirth = user.DateOfBirth ?? DateTime.MinValue,
                Email = user.Email
            };

            return new ProfileSettingsDto
            {
                AddressInformations = addressinfo,
                PersonalInformations = personalinfo,
                Profilepicture = imageinfo
            };
        }



        public async Task<bool> UpdateProfileSettings(string userId, UpdateProfileSettingsDto dto)
        {
            var user = await _user.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            var address = await _addressrepo.FindAddressById(userId);

            if (address == null && dto.AddressInformations != null)
            {
                address = new Address
                {
                    AppUserId = userId
                };
                await _addressrepo.Add(address);
            }

            if (dto.PersonalInformations != null)
            {
                var p = dto.PersonalInformations;

                if (!string.IsNullOrWhiteSpace(p.FullName))
                    user.FullName = p.FullName;

                if (!string.IsNullOrWhiteSpace(p.PhoneNumber))
                    user.PhoneNumber = p.PhoneNumber;

                if (p.DateOfBirth.HasValue)
                    user.DateOfBirth = p.DateOfBirth.Value;

                if (!string.IsNullOrWhiteSpace(p.Email))
                    user.Email = p.Email;
            }

            if (dto.Profilepicture != null)
            {
                var img = dto.Profilepicture;

                if (!string.IsNullOrWhiteSpace(img.ImageUrl))
                    user.ProfileImage = img.ImageUrl;
            }

            if (dto.AddressInformations != null && address != null)
            {
                var a = dto.AddressInformations;

                if (!string.IsNullOrWhiteSpace(a.StreetAddress))
                    address.Street = a.StreetAddress;

                if (!string.IsNullOrWhiteSpace(a.City))
                    address.City = a.City;

                if (!string.IsNullOrWhiteSpace(a.Country))
                    address.Country = a.Country;

                if (a.ZipCode.HasValue)
                    address.ZipCode = a.ZipCode.Value;
            }

            var result = await _user.UpdateAsync(user);
            if (!result.Succeeded)
                throw new Exception("Failed to update user profile");
            
            await _addressrepo.SaveChangesAsync();

            return true;
        }





    }
}
