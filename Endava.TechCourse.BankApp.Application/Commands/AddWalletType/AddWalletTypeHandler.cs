using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastracture.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Application.Commands.AddWalletType
{
    public class AddWalletTypeHandler : IRequestHandler<AddWalletTypeCommand, CommandStatus>
    {
        private readonly ApplicationDbContext _context;

        public AddWalletTypeHandler(ApplicationDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            _context = context;
        }

        public async Task<CommandStatus> Handle(AddWalletTypeCommand request, CancellationToken cancellationToken)
        {
            var checkWalletTypeName = await _context.WalletType.AnyAsync(x => x.TypeName == request.TypeName, cancellationToken);
            if (checkWalletTypeName)
            {
                return CommandStatus.Failed("Wallet type with this name already exists");
            }

            var walletType = new WalletType()
            {
                TypeName = request.TypeName,
                Commision = request.Commision,
                CanBeRemoved = true
            };

            await _context.WalletType.AddAsync(walletType, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new CommandStatus();
        }
    }
}