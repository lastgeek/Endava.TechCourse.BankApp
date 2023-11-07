using Endava.TechCourse.BankApp.Domain.Models;
using MediatR;

public class GetWalletByIdQuery : IRequest<Wallet>
{
    public Guid Id { get; set; }
}