using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.DeleteWallet
{
    public class DeleteWalletCommand : IRequest<bool>
    {
        public Guid WalletId { get; set; }
    }
}