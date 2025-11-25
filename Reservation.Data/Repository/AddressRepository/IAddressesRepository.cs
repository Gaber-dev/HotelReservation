using Microsoft.EntityFrameworkCore;
using Reservation.Data.Model.UserAddress;
using Reservation.Data.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Data.Repository.AddressRepository
{
    public interface IAddressesRepository : IGenericRepository<Address>
    {
        Task<Address?> FindAddressById(string userId);

        Task AddAsync(Address address);

        Task SaveChangesAsync();
    }
}
