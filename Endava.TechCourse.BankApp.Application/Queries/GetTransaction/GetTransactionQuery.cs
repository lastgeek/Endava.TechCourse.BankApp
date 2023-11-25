using Endava.TechCourse.BankApp.Domain.Models;
using MediatR;

namespace Endava.TechCourse.BankApp.Application.Queries.GetTransaction
{
    public class GetTransactionQuery : IRequest<List<Transaction>>
    {
        public string walletCode { get; set; }
    }
}