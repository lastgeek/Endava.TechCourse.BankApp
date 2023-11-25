using Endava.TechCourse.BankApp.Application.Commands.MakeTransaction;
using Endava.TechCourse.BankApp.Application.Queries.GetTransaction;
using Endava.TechCourse.BankApp.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Endava.TechCourse.BankApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(mediator);
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> MakeTransaction([FromBody] WalletTransactionDTO walletTransactionDTO)
        {
            var command = new MakeTransactionCommand()
            {
                SenderWalletCode = walletTransactionDTO.SenderWalletCode,
                ReceiverWalletCode = walletTransactionDTO.ReceiverWalletCode,
                Amount = walletTransactionDTO.Amount,
                CurrencyId = walletTransactionDTO.CurrencyId
            };

            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("{walletCode}")]
        public async Task<List<WalletTransactionDTO>> GetTransaction(string walletCode)
        {
            var query = new GetTransactionQuery { walletCode = walletCode };

            var transactions = await _mediator.Send(query);
            var dtos = new List<WalletTransactionDTO>();

            foreach (var transaction in transactions)
            {
                var dto = new WalletTransactionDTO()
                {
                    SenderWalletCode = transaction.SourceWalletCode,
                    ReceiverWalletCode = transaction.DestinationWalletCode,
                    Amount = walletCode == transaction.SourceWalletCode ? -transaction.Amount : transaction.Amount,
                    CurrencyCode = transaction.Currency.CurrencyCode,
                    CreationDate = transaction.CreationDate
                };
                dtos.Add(dto);
            }

            return dtos.OrderByDescending(x => x.CreationDate).ToList();
        }
    }
}