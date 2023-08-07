using Ada.Kanban.Db.Entities;

namespace Ada.Kanban.Db.Repositories
{
    public interface ICardRepository
    {
        Task<Card> CreateCardAsync(Card card);
        Task<List<Card>> DeleteCardAsync(Card card);
        Task<List<Card>> GetAllCardsAsync();
        Task<Card> GetCardByIdAsync(Guid cardId);
        Task<Card> UpdateCardAsync(Card card);
    }
}