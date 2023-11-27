using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastracture.Persistance;
using MediatR;

namespace Endava.TechCourse.BankApp.Application.Queries.GetWalletTypeById
{
    public class GetWalletTypeByIdHandler : IRequestHandler<GetWalletTypeByIdQuery, WalletType>
    {
        private readonly ApplicationDbContext _context;

        public GetWalletTypeByIdHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<WalletType> Handle(GetWalletTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var walletType = await _context.WalletType.FindAsync(request.WalletTypeId);

            if (walletType == null)
            {
                return null;
            }

            return walletType;
        }
    }
}