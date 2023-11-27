using Endava.TechCourse.BankApp.Application.Commands.MakeTransaction;
using Endava.TechCourse.BankApp.Application.Queries.GetAllWallets;
using Endava.TechCourse.BankApp.Application.Queries.GetTransaction;
using Endava.TechCourse.BankApp.Application.Queries.GetUserMainWallet;
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
        public async Task<IActionResult> MakeTransaction([FromBody] MakeTransactionDTO makeTransactionDTO)
        {
            var query = new GetAllWalletsQuery();
            var wallets = await _mediator.Send(query);
            var wallet = wallets.FirstOrDefault(w => w.Code == makeTransactionDTO.ReceiverWalletCode && w.Active);

            if (makeTransactionDTO.ReceiverEmail != null)
            {
                var emailquery = new GetUserMainWalletQuery { UserEmail = makeTransactionDTO.ReceiverEmail };
                wallet = await _mediator.Send(emailquery);
            }

            var AmountMdl = makeTransactionDTO.SenderWallet.Amount * makeTransactionDTO.SenderCurrency.ChangeRate;
            var canMakeTransaction = AmountMdl / makeTransactionDTO.Currency.ChangeRate >= makeTransactionDTO.Amount;

            if (canMakeTransaction && wallet != null)
            {
                var command = new MakeTransactionCommand()
                {
                    SenderWalletCode = makeTransactionDTO.SenderWallet.Code,
                    ReceiverWalletCode = wallet.Code,
                    Amount = makeTransactionDTO.Amount,
                    CurrencyId = new Guid(makeTransactionDTO.Currency.Id)
                };

                await _mediator.Send(command);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
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
                    Commission = transaction.Commission,
                    CurrencyCode = transaction.Currency.CurrencyCode,
                    CreationDate = transaction.CreationDate
                };
                dtos.Add(dto);
            }

            return dtos.OrderByDescending(x => x.CreationDate).ToList();
        }
    }
}