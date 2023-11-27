using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastracture.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Application.Queries.GetUserMainWallet
{
    public class GetUserMainWalletHandler : IRequestHandler<GetUserMainWalletQuery, Wallet>
    {
        private readonly ApplicationDbContext _context;

        public GetUserMainWalletHandler(ApplicationDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            _context = context;
        }

        public async Task<Wallet> Handle(GetUserMainWalletQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == request.UserEmail);
            if (user == null)
            {
                return null;
            }
            var wallets = await _context.Wallets.AsNoTracking().Where(w => w.UserId == user.Id).ToListAsync(cancellationToken);
            var mainWallet = wallets.FirstOrDefault(w => w.MainWallet);

            return mainWallet;
        }
    }
}