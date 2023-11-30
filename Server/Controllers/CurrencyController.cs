using Endava.TechCourse.BankApp.Application.Commands.AddCurrency;
using Endava.TechCourse.BankApp.Application.Commands.DeleteCurrency;
using Endava.TechCourse.BankApp.Application.Commands.UpdateCurrency;
using Endava.TechCourse.BankApp.Application.Queries.GetCurrencies;
using Endava.TechCourse.BankApp.Application.Queries.GetCurrencyById;
using Endava.TechCourse.BankApp.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Endava.TechCourse.BankApp.Server.Controllers
{
    [Route("api/currencies")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CurrencyController(IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(mediator);
            _mediator = mediator;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrency(Guid id)
        {
            var command = new DeleteCurrencyCommand { CurrencyId = id };
            var result = await _mediator.Send(command);

            if (result.IsSuccessful)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCurrency([FromBody] CurrencyDTO currencyDTO)
        {
            var command = new AddCurrencyCommand()
            {
                Name = currencyDTO.Name,
                CurrencyCode = currencyDTO.CurrencyCode,
                ChangeRate = currencyDTO.ChangeRate,
            };

            var result = await _mediator.Send(command);

            return result.IsSuccessful ? Ok(result) : BadRequest(result.Error);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCurrency(Guid id, [FromBody] UpdateCurrencyCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCurrencyById(Guid id)
        {
            var query = new GetCurrencyByIdQuery { CurrencyId = id };
            var currency = await _mediator.Send(query);

            if (currency == null)
            {
                return NotFound();
            }

            var dto = new CurrencyDTO()
            {
                CanBeRemoved = currency.CanBeRemoved,
                ChangeRate = currency.CurrencyRate,
                CurrencyCode = currency.CurrencyCode,
                Id = currency.Id.ToString(),
                Name = currency.Name,
            };

            return Ok(dto);
        }

        [HttpGet]
        public async Task<List<CurrencyDTO>> GetCurrencies()
        {
            var query = new GetCurrenciesQuery();

            var currencies = await _mediator.Send(query);
            var dtos = new List<CurrencyDTO>();

            foreach (var currency in currencies)
            {
                var dto = new CurrencyDTO()
                {
                    CanBeRemoved = currency.CanBeRemoved,
                    ChangeRate = currency.CurrencyRate,
                    CurrencyCode = currency.CurrencyCode,
                    Id = currency.Id.ToString(),
                    Name = currency.Name,
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}