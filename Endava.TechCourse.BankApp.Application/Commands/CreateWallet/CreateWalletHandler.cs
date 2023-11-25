using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastracture.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Application.Commands.CreateWallet
{
    public class CreateWalletHandler : IRequestHandler<CreateWalletCommand>
    {
        private readonly ApplicationDbContext _context;

        public CreateWalletHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateWalletCommand request, CancellationToken cancellationToken)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == new Guid(request.UserId));
            var currency = _context.Currency.FirstOrDefault(c => c.CurrencyCode == request.CurrencyCode);
            if (currency == null || user == null)
            {
                throw new Exception($"Currency with code '{request.CurrencyCode}' " +
                    $"or user with id '{request.UserId}' not found.");
            }
            var wallet = new Wallet
            {
                Code = request.Code,
                Type = request.Type,
                Amount = request.Amount,
                Currency = currency,
                UserId = new Guid(request.UserId)
            };

            var userWallets = await _context.Wallets
                .Where(w => w.UserId == user.Id)
                .ToListAsync();

            wallet.MainWallet = userWallets.Count == 0;

            await _context.Wallets.AddAsync(wallet, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}