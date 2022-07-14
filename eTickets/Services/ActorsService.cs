using eTickets.Data;
using eTickets.Data.Base;
using eTickets.Interfaces;
using eTickets.Models;

namespace eTickets.Services
{
    public class ActorsService : EntityBaseRepository<Actor>, IActorsService
    {
        public ActorsService(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}