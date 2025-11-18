using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Data.Repository.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task Add(T entity);
    }
}
