using Ada.Kanban.Service.Models;

namespace Ada.Kanban.Service.Services
{
    public interface ICardService
    {
        Task<CardModel> CreateCardAsync(CreateCardModel cardModel);
        Task<List<CardModel>> DeleteCardByIdAsync(Guid cardId);
        Task<List<CardModel>> GetAllCardsAsync();
        Task<CardModel> UpdateCardByIdAsync(UpdateCardModel cardModel, Guid cardId);
    }
}