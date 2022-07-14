using eCommerceApp.Data;
using eCommerceApp.Data.Base;
using eCommerceApp.Interfaces;
using eCommerceApp.Models;

namespace eCommerceApp.Services
{
    public class ProducersService : EntityBaseRepository<Producer>, IProducersService
    {
        public ProducersService(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}