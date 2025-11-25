using Reservation.Domain.Dtos.AminityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Domain.Managers.AminityManager
{
    public interface IAminityService
    {
        Task AddAminity(AddAminityDto dtofromuser);
    }
}
