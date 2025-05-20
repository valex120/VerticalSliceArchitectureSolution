using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CompanyManagementApp.Data;
using CompanyManagementApp.Features.Companies;
using System.Threading.Tasks;

namespace CompanyManagementApp.Pages.Companies
{
    public class EditModel : PageModel
    {
        private readonly CompanyDbContext _dbContext;

        public EditModel(CompanyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public class InputModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var company = await _dbContext.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            Input = new InputModel
            {
                Id = company.Id,
                Name = company.Name,
                Address = company.Address,
                PhoneNumber = company.PhoneNumber,
                Email = company.Email
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var handler = new EditCompany.Handler(_dbContext);
            var command = new EditCompany.Command(Input.Id, Input.Name, Input.Address, Input.PhoneNumber, Input.Email);
            var response = await handler.Handle(command);

            if (!response.Success)
            {
                ModelState.AddModelError(string.Empty, response.Message);
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}
