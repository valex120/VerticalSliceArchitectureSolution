using System.Threading.Tasks;
using CompanyManagementApp.Data;
using CompanyManagementApp.Models;

namespace CompanyManagementApp.Features.Companies
{
    /// <summary>
    /// Вертикальный срез для создания компании.
    /// </summary>
    public class CreateCompany
    {
        public record Command(string Name, string Address, string PhoneNumber, string Email);

        public record Response(int CompanyId, string Message);

        public class Handler
        {
            private readonly CompanyDbContext _context;
            public Handler(CompanyDbContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Command command)
            {
                var company = new Company
                {
                    Name = command.Name,
                    Address = command.Address,
                    PhoneNumber = command.PhoneNumber,
                    Email = command.Email
                };

                _context.Companies.Add(company);
                await _context.SaveChangesAsync();
                return new Response(company.Id, "Компания успешно создана.");
            }
        }
    }
}