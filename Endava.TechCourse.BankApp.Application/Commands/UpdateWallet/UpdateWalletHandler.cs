using Endava.TechCourse.BankApp.Infrastracture.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Application.Commands.UpdateWallet
{
    public class UpdateWalletHandler : IRequestHandler<UpdateWalletCommand>
    {
        private readonly ApplicationDbContext _context;

        public UpdateWalletHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateWalletCommand request, CancellationToken cancellationToken)
        {
            var wallet = await _context.Wallets.FindAsync(request.WalletId);

            if (wallet == null)
            {
                throw new Exception("Wallet not found");
            }

            wallet.Type = request.Type;
            wallet.Amount = request.Amount;

            if (!string.IsNullOrEmpty(request.CurrencyCode))
            {
                var currency = await _context.Currency.FirstOrDefaultAsync(c => c.CurrencyCode == request.CurrencyCode);
                if (currency == null)
                {
                    throw new Exception("Currency not found");
                }
                wallet.Currency = currency;
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}