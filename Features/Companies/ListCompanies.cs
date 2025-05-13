using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyManagementApp.Data;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagementApp.Features.Companies
{
    /// <summary>
    /// Вертикальный срез для получения списка компаний.
    /// </summary>
    public class ListCompanies
    {
        public class Query { }

        public record ResponseDTO(int Id, string Name, string Address, string PhoneNumber, string Email);

        public class Handler
        {
            private readonly CompanyDbContext _context;
            public Handler(CompanyDbContext context)
            {
                _context = context;
            }

            public async Task<List<ResponseDTO>> Handle(Query query)
            {
                return await _context.Companies
                    .Select(c => new ResponseDTO(c.Id, c.Name, c.Address, c.PhoneNumber, c.Email))
                    .ToListAsync();
            }


        }
    }
}