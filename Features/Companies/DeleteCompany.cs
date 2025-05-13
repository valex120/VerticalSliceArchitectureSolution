using System.Threading.Tasks;
using CompanyManagementApp.Data;
using CompanyManagementApp.Models;

namespace CompanyManagementApp.Features.Companies
{
    /// <summary>
    /// Вертикальный срез для удаления компании.
    /// </summary>
    public class DeleteCompany
    {
        public record Command(int CompanyId);

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
                var company = await _context.Companies.FindAsync(command.CompanyId);
                if (company == null)
                {
                    return new Response(false, "Компания не найдена.");
                }
                _context.Companies.Remove(company);
                await _context.SaveChangesAsync();
                return new Response(true, "Компания успешно удалена.");
            }
        }
    }
}