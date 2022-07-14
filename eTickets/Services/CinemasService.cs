using eTickets.Data;
using eTickets.Data.Base;
using eTickets.Interfaces;
using eTickets.Models;

namespace eTickets.Services
{
    public class CinemasService : EntityBaseRepository<Cinema>, ICinemasService
    {
        public CinemasService(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
