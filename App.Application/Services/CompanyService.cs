using App.Application.Interfaces;
using App.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repository;

        public CompanyService(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Company> GetCompanyByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateCompanyAsync(Company company)
        {
            await _repository.AddAsync(company);
        }

        public async Task UpdateCompanyAsync(Company company)
        {
            await _repository.UpdateAsync(company);
        }

        public async Task DeleteCompanyAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
