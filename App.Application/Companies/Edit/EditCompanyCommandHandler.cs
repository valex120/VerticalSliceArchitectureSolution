using App.Infrastructure.Data;
using System;

namespace App.Features.Companies.Edit
{
    public class EditCompanyCommandHandler
    {
        private readonly AppDbContext _dbContext;

        public EditCompanyCommandHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(EditCompanyCommand command)
        {
            var company = await _dbContext.Companies.FindAsync(command.Id);
            if (company == null)
            {
                throw new Exception($"Компания с ID {command.Id} не найдена.");
            }

            company.Name = command.Name;
            company.Address = command.Address;
            company.PhoneNumber = command.PhoneNumber;
            company.Email = command.Email;

            await _dbContext.SaveChangesAsync();
        }
    }
}
