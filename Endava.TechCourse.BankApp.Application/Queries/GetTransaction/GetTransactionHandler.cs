using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastracture.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Application.Queries.GetTransaction
{
    public class GetTransactionHandler : IRequestHandler<GetTransactionQuery, List<Transaction>>
    {
        private readonly ApplicationDbContext _context;

        public GetTransactionHandler(ApplicationDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            _context = context;
        }

        public async Task<List<Transaction>> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
        {
            var transactions = await _context.Transactions
                .Include(w => w.Currency)
                .AsNoTracking()
                .Where(t => t.SourceWalletCode == request.walletCode || t.DestinationWalletCode == request.walletCode)
                .ToListAsync();

            return transactions;
        }
    }
}