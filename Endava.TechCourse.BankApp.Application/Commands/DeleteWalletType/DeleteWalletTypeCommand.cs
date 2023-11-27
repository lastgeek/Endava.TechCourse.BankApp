using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.DeleteWalletType
{
    public class DeleteWalletTypeCommand : IRequest<bool>
    {
        public Guid WalletTypeId { get; set; }
    }
}