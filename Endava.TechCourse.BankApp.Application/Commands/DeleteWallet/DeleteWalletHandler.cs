using Endava.TechCourse.BankApp.Infrastracture.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Application.Commands.DeleteWallet
{
    public class DeleteWalletHandler : IRequestHandler<DeleteWalletCommand, CommandStatus>
    {
        private readonly ApplicationDbContext _context;

        public DeleteWalletHandler(ApplicationDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            _context = context;
        }

        public async Task<CommandStatus> Handle(DeleteWalletCommand request, CancellationToken cancellationToken)
        {
            var wallet = await _context.Wallets.FindAsync(request.WalletId, cancellationToken);

            if (wallet == null)
            {
                return CommandStatus.Failed("Wallet was not found.");
            }
            if (wallet.Active == true)
            {
                return CommandStatus.Failed("Wallet cannot be delete. Wallet is active.");
            }

            var currency = await _context.Currency.FirstOrDefaultAsync(c => c.Id == wallet.CurrencyId, cancellationToken);
            if (currency == null)
            {
                return CommandStatus.Failed("Currency was not found.");
            }

            _context.Wallets.Remove(wallet);
            await _context.SaveChangesAsync(cancellationToken);

            if (currency.Wallets.Count == 0) currency.CanBeRemoved = true;

            return new CommandStatus();
        }
    }
}