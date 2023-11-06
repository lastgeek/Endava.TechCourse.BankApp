using Endava.TechCourse.BankApp.Application.Commands.AddCurrency;
using Endava.TechCourse.BankApp.Infrastracture.Persistance;
using Endava.TechCourse.BankApp.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Server.Controllers
{
    [Route("api/currencies")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;

        public CurrencyController(ApplicationDbContext dbContext, IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(dbContext);
            ArgumentNullException.ThrowIfNull(mediator);
            _context = dbContext;
            _mediator = mediator;
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

        [HttpGet]
        public async Task<List<CurrencyDTO>> GetCurrencies()
        {
            var currencies = await _context.Currency.ToListAsync();
            var dtos = new List<CurrencyDTO>();

            foreach (var currency in currencies)
            {
                var dto = new CurrencyDTO()
                {
                    CanBeRemoved = true,
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