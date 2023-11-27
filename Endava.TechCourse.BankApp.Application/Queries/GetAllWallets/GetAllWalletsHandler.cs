using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastracture.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Application.Queries.GetAllWallets
{
    public class GetAllWalletsHandler : IRequestHandler<GetAllWalletsQuery, List<Wallet>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllWalletsHandler(ApplicationDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            _context = context;
        }

        public async Task<List<Wallet>> Handle(GetAllWalletsQuery request, CancellationToken cancellationToken)
        {
            var wallets = await _context.Wallets.Include(w => w.Currency).AsNoTracking().ToListAsync(cancellationToken);

            return wallets;
        }
    }
}