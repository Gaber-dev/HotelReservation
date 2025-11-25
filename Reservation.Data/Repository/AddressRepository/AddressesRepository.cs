using Microsoft.EntityFrameworkCore;
using Reservation.Data.Data;
using Reservation.Data.Model.UserAddress;
using Reservation.Data.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Data.Repository.AddressRepository
{
    public class AddressesRepository : GenericRepository<Address> , IAddressesRepository 
    {
        private readonly ApplicationDbContext _context;

        public AddressesRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<Address?> FindAddressById(string userId)
        {
            var address = await _context.Address.FirstOrDefaultAsync(a => a.AppUserId == userId);
            return address;
        }

        public async Task AddAsync(Address address)
        {
            await _context.Address.AddAsync(address);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
