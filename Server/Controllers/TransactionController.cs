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
                SenderWalletId = walletTransactionDTO.SenderWalletId,
                ReceiverWalletId = walletTransactionDTO.ReceiverWalletId,
                Amount = walletTransactionDTO.Amount,
                CurrencyId = walletTransactionDTO.CurrencyId
            };

            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("{walletId}")]
        public async Task<List<WalletTransactionDTO>> GetTransaction(Guid walletId)
        {
            var query = new GetTransactionQuery();

            var transactions = await _mediator.Send(query);
            var dtos = new List<WalletTransactionDTO>();

            foreach (var transaction in transactions)
            {
                if (transaction.SourceWalletId == walletId)
                {
                    var dto = new WalletTransactionDTO()
                    {
                        SenderWalletId = transaction.SourceWalletId,
                        ReceiverWalletId = transaction.DestinationWalletId,
                        Amount = -transaction.Amount,
                        CurrencyCode = transaction.Currency.CurrencyCode
                    };
                    dtos.Add(dto);
                }
                else if (transaction.DestinationWalletId == walletId)
                {
                    var dto = new WalletTransactionDTO()
                    {
                        SenderWalletId = transaction.SourceWalletId,
                        ReceiverWalletId = transaction.DestinationWalletId,
                        Amount = transaction.Amount,
                        CurrencyCode = transaction.Currency.CurrencyCode
                    };
                    dtos.Add(dto);
                }
            }

            return dtos;
        }
    }
}