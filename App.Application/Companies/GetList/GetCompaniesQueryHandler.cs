using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using App.Infrastructure.Data;

namespace App.Features.Companies.GetList
{
    public class GetCompaniesQueryHandler
    {
        private readonly AppDbContext _dbContext;

        public GetCompaniesQueryHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Company>> Handle(GetCompaniesQuery query)
        {
            return await _dbContext.Companies.ToListAsync();
        }
    }
}
