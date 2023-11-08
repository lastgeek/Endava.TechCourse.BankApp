using Endava.TechCourse.BankApp.Infrastracture.Persistance;
using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.DeleteCurrency
{
    public class DeleteCurrencyHandler : IRequestHandler<DeleteCurrencyCommand, bool>
    {
        private readonly ApplicationDbContext _context;

        public DeleteCurrencyHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
        {
            var currency = await _context.Currency.FindAsync(request.CurrencyId);

            if (currency == null)
            {
                return false;
            }

            _context.Currency.Remove(currency);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}