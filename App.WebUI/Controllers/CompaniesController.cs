using Microsoft.AspNetCore.Mvc;
using App.Features.Companies.Create;
using App.Features.Companies.Edit;
using App.Features.Companies.Delete;
using App.Features.Companies.GetList;

namespace App.WebUI.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly CreateCompanyCommandHandler _createHandler;
        private readonly EditCompanyCommandHandler _editHandler;
        private readonly DeleteCompanyCommandHandler _deleteHandler;
        private readonly GetCompaniesQueryHandler _getListHandler;

        public CompaniesController(
            CreateCompanyCommandHandler createHandler,
            EditCompanyCommandHandler editHandler,
            DeleteCompanyCommandHandler deleteHandler,
            GetCompaniesQueryHandler getListHandler)
        {
            _createHandler = createHandler;
            _editHandler = editHandler;
            _deleteHandler = deleteHandler;
            _getListHandler = getListHandler;
        }

        // GET: Companies
        public async Task<IActionResult> Index()
        {
            var query = new GetCompaniesQuery();
            var companies = await _getListHandler.Handle(query);
            return View(companies);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Features.Companies.Create.CreateCompanyCommand command)
        {
            if (ModelState.IsValid)
            {
                await _createHandler.Handle(command);
                return RedirectToAction(nameof(Index));
            }
            return View(command);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var companies = await _getListHandler.Handle(new GetCompaniesQuery());
            var company = companies.FirstOrDefault(c => c.Id == id);

            if (company == null)
                return NotFound();

            var command = new App.Features.Companies.Edit.EditCompanyCommand
            {
                Id = company.Id,
                Name = company.Name,
                Address = company.Address,
                PhoneNumber = company.PhoneNumber,
                Email = company.Email
            };

            return View(command);
        }

        // POST: Companies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Features.Companies.Edit.EditCompanyCommand command)
        {
            if (id != command.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _editHandler.Handle(command);
                return RedirectToAction(nameof(Index));
            }
            return View(command);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var companies = await _getListHandler.Handle(new GetCompaniesQuery());
            var company = companies.FirstOrDefault(c => c.Id == id);
            if (company == null)
                return NotFound();
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _deleteHandler.Handle(new Features.Companies.Delete.DeleteCompanyCommand { Id = id });
            return RedirectToAction(nameof(Index));
        }
    }
}
