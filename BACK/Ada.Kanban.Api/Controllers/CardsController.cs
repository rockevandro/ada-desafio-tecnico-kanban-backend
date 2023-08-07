using Ada.Kanban.Service.Models;
using Ada.Kanban.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ada.Kanban.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;
        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cardList = await _cardService.GetAllCardsAsync();
            return Ok(cardList);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateCardModel createCard)
        {
            var card = await _cardService.CreateCardAsync(createCard);
            return Ok(card);
        }

        [HttpPut("{cardId}")]
        public async Task<IActionResult> Put(UpdateCardModel updateCard, Guid cardId)
        {
            var card = await _cardService.UpdateCardByIdAsync(updateCard, cardId);
            return Ok(card);
        }

        [HttpDelete("{cardId}")]
        public async Task<IActionResult> Delete(Guid cardId)
        {
            var cardList = await _cardService.DeleteCardByIdAsync(cardId);
            return Ok(cardList);
        }

    }
}
