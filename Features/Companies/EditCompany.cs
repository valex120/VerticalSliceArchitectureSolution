using System.Threading.Tasks;
using CompanyManagementApp.Data;
using CompanyManagementApp.Models;

namespace CompanyManagementApp.Features.Companies
{
    /// <summary>
    /// Вертикальный срез для редактирования компании.
    /// </summary>
    public class EditCompany
    {
        public record Command(int Id, string Name, string Address, string PhoneNumber, string Email);

        public record Response(bool Success, string Message);

        public class Handler
        {
            private readonly CompanyDbContext _context;
            public Handler(CompanyDbContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Command command)
            {
                var company = await _context.Companies.FindAsync(command.Id);
                if (company == null)
                {
                    return new Response(false, "Компания не найдена.");
                }

                company.Name = command.Name;
                company.Address = command.Address;
                company.PhoneNumber = command.PhoneNumber;
                company.Email = command.Email;

                await _context.SaveChangesAsync();
                return new Response(true, "Компания успешно обновлена.");
            }
        }
    }
}