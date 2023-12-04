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
            ArgumentNullException.ThrowIfNull(context);
            _context = context;
        }

        public async Task Handle(UpdateWalletCommand request, CancellationToken cancellationToken)
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.Code == request.WalletCode, cancellationToken)
                ?? throw new Exception("Wallet not found");
            var currency = await _context.Currency.FirstOrDefaultAsync(c => c.Id == request.Currency, cancellationToken)
                ?? throw new Exception("Currency not found");
            var walletCurrency = await _context.Currency.FirstOrDefaultAsync(c => c.Id == wallet.CurrencyId, cancellationToken)
                ?? throw new Exception("Wallet currency not found");

            var conversedAmount = request.UpdateAmount * currency.CurrencyRate;
            conversedAmount /= walletCurrency.CurrencyRate;

            wallet.Amount += conversedAmount;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}