using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastracture.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Application.Queries.GetWalletType
{
    public class GetWalletTypeHandler : IRequestHandler<GetWalletTypeQuery, List<WalletType>>
    {
        private readonly ApplicationDbContext _context;

        public GetWalletTypeHandler(ApplicationDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            _context = context;
        }

        public async Task<List<WalletType>> Handle(GetWalletTypeQuery request, CancellationToken cancellationToken)
        {
            var walletTypes = await _context.WalletType.ToListAsync(cancellationToken);

            return walletTypes;
        }
    }
}