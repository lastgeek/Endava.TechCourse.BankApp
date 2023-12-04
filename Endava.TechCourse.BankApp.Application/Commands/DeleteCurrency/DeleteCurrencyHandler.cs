using Endava.TechCourse.BankApp.Infrastracture.Persistance;
using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.DeleteCurrency
{
    public class DeleteCurrencyHandler : IRequestHandler<DeleteCurrencyCommand, CommandStatus>
    {
        private readonly ApplicationDbContext _context;

        public DeleteCurrencyHandler(ApplicationDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            _context = context;
        }

        public async Task<CommandStatus> Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
        {
            var currency = await _context.Currency.FindAsync(request.CurrencyId, cancellationToken);

            if (currency == null)
            {
                return CommandStatus.Failed("Currency was not found.");
            }
            if (!currency.CanBeRemoved)
            {
                return CommandStatus.Failed("Currency cannot be removed.");
            }

            _context.Currency.Remove(currency);
            await _context.SaveChangesAsync(cancellationToken);

            return new CommandStatus();
        }
    }
}