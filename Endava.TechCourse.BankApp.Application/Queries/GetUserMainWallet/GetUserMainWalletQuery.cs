using Endava.TechCourse.BankApp.Domain.Models;
using MediatR;

namespace Endava.TechCourse.BankApp.Application.Queries.GetUserMainWallet
{
    public class GetUserMainWalletQuery : IRequest<Wallet>
    {
        public string UserEmail { get; set; }
    }
}