using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastracture.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Application.Commands.AddCurrency
{
    public class AddCurrencyHandler : IRequestHandler<AddCurrencyCommand, CommandStatus>
    {
        private readonly ApplicationDbContext _context;

        public AddCurrencyHandler(ApplicationDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            _context = context;
        }

        public async Task<CommandStatus> Handle(AddCurrencyCommand request, CancellationToken cancellationToken)
        {
            var checkCurrencyByName = await _context.Currency.AnyAsync(x => x.Name == request.Name, cancellationToken);
            if (checkCurrencyByName)
            {
                return CommandStatus.Failed("Currency with this name already exists");
            }
            var checkCurrencyByCode = await _context.Currency.AnyAsync(x => x.CurrencyCode == request.CurrencyCode, cancellationToken);
            if (checkCurrencyByCode)
            {
                return CommandStatus.Failed("Currency with this currency code already exists");
            }

            var currency = new Currency()
            {
                Name = request.Name,
                CurrencyCode = request.CurrencyCode,
                CurrencyRate = request.ChangeRate,
                CanBeRemoved = true
            };

            await _context.Currency.AddAsync(currency, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new CommandStatus();
        }
    }
}