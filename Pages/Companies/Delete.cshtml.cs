using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CompanyManagementApp.Data;
using CompanyManagementApp.Features.Companies;
using System.Threading.Tasks;

namespace CompanyManagementApp.Pages.Companies
{
    public class DeleteModel : PageModel
    {
        private readonly CompanyDbContext _context;
        public DeleteModel(CompanyDbContext context)
        {
            _context = context;
        }

        public class DeleteInputModel
        {
            public int CompanyId { get; set; }
        }

        [BindProperty]
        public DeleteInputModel Input { get; set; }
        public string Message { get; set; }

        public async Task OnGetAsync(int id)
        {
            Input = new DeleteInputModel { CompanyId = id };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var handler = new DeleteCompany.Handler(_context);
            var command = new DeleteCompany.Command(Input.CompanyId);
            var result = await handler.Handle(command);
            Message = result.Message;
            return RedirectToPage("Index");
        }
    }
}