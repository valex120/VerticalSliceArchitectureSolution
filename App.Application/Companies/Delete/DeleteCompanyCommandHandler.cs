using App.Infrastructure.Data;
using System;

namespace App.Features.Companies.Delete
{
    public class DeleteCompanyCommandHandler
    {
        private readonly AppDbContext _dbContext;

        public DeleteCompanyCommandHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteCompanyCommand command)
        {
            var company = await _dbContext.Companies.FindAsync(command.Id);
            if (company == null)
            {
                throw new Exception($"Компания с ID {command.Id} не найдена.");
            }

            _dbContext.Companies.Remove(company);
            await _dbContext.SaveChangesAsync();
        }
    }
}
