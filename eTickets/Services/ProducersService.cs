using eTickets.Data;
using eTickets.Data.Base;
using eTickets.Interfaces;
using eTickets.Models;

namespace eTickets.Services
{
    public class ProducersService : EntityBaseRepository<Producer>, IProducersService
    {
        public ProducersService(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
