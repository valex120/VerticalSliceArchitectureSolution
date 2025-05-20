using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CompanyManagementApp.Data;
using CompanyManagementApp.Features.Companies;
using System.Threading.Tasks;

namespace CompanyManagementApp.Pages.Companies
{
    public class CreateModel : PageModel
    {
        private readonly CompanyDbContext _dbContext;

        public CreateModel(CompanyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public class InputModel
        {
            public string Name { get; set; }
            public string Address { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public void OnGet()
        {
            Input = new InputModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var handler = new CreateCompany.Handler(_dbContext);
            var command = new CreateCompany.Command(Input.Name, Input.Address, Input.PhoneNumber, Input.Email);
            var response = await handler.Handle(command);

            if (response.CompanyId <= 0)
            {
                ModelState.AddModelError(string.Empty, "Ошибка при создании компании.");
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}