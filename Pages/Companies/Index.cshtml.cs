using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CompanyManagementApp.Data;
using CompanyManagementApp.Models;
using CompanyManagementApp.Features.Companies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyManagementApp.Pages.Companies
{
    public class IndexModel : PageModel
    {
        private readonly CompanyDbContext _dbContext;

        public IndexModel(CompanyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ListCompanies.ResponseDTO> Companies { get; set; } 


        public async Task OnGetAsync()
        {
            var handler = new ListCompanies.Handler(_dbContext);
            Companies = await handler.Handle(new ListCompanies.Query());
        }
    }
}