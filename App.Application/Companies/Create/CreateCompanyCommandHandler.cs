using App.Domain.Entities;
using App.Infrastructure.Data;
using System;

namespace App.Features.Companies.Create
{
    public class CreateCompanyCommandHandler
    {
        private readonly AppDbContext _dbContext;

        public CreateCompanyCommandHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(CreateCompanyCommand command)
        {
            var company = new Company
            {
                Name = command.Name,
                Address = command.Address,
                PhoneNumber = command.PhoneNumber,
                Email = command.Email
            };

            _dbContext.Companies.Add(company);
            await _dbContext.SaveChangesAsync();
        }
    }
}
