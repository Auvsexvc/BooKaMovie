using eCommerceApp.Data;
using eCommerceApp.Data.Base;
using eCommerceApp.Interfaces;
using eCommerceApp.Models;

namespace eCommerceApp.Services
{
    public class CinemasService : EntityBaseRepository<Cinema>, ICinemasService
    {
        public CinemasService(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}