using Endava.TechCourse.BankApp.Infrastracture.Persistance;
using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.DeleteWallet
{
    public class DeleteWalletHandler : IRequestHandler<DeleteWalletCommand, bool>
    {
        private readonly ApplicationDbContext _context;

        public DeleteWalletHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteWalletCommand request, CancellationToken cancellationToken)
        {
            var wallet = await _context.Wallets.FindAsync(request.WalletId);

            if (wallet == null || wallet.Active == true)
            {
                return false;
            }

            var currency = _context.Currency.FirstOrDefault(c => c.Id == wallet.CurrencyId);

            _context.Wallets.Remove(wallet);
            await _context.SaveChangesAsync(cancellationToken);
            if (currency.Wallets.Count == 0) currency.CanBeRemoved = true;

            return true;
        }
    }
}