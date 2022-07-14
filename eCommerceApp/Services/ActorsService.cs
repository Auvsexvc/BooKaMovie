using eCommerceApp.Data;
using eCommerceApp.Data.Base;
using eCommerceApp.Interfaces;
using eCommerceApp.Models;

namespace eCommerceApp.Services
{
    public class ActorsService : EntityBaseRepository<Actor>, IActorsService
    {
        public ActorsService(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}