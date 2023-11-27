using Endava.TechCourse.BankApp.Infrastracture.Persistance;
using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.DeleteWalletType
{
    public class DeleteWalletTypeHandler : IRequestHandler<DeleteWalletTypeCommand, bool>
    {
        private readonly ApplicationDbContext _context;

        public DeleteWalletTypeHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteWalletTypeCommand request, CancellationToken cancellationToken)
        {
            var walletType = await _context.WalletType.FindAsync(request.WalletTypeId);

            if (walletType == null || !walletType.CanBeRemoved)
            {
                return false;
            }

            _context.WalletType.Remove(walletType);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}