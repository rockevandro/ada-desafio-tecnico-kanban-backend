using Ada.Kanban.Common.Exceptions;
using Ada.Kanban.Db.DbContexts;
using Ada.Kanban.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ada.Kanban.Db.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly AdaKanbanDbContext _dbContext;

        public CardRepository(AdaKanbanDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Card> CreateCardAsync(Card card)
        {
            var createdCard = await _dbContext.Cards.AddAsync(card);
            await _dbContext.SaveChangesAsync();

            return createdCard.Entity;
        }

        public async Task<Card> UpdateCardAsync(Card card)
        {
            var updatedCard = _dbContext.Cards.Update(card);
            await _dbContext.SaveChangesAsync();

            return updatedCard.Entity;
        }

        public async Task<List<Card>> DeleteCardAsync(Card card)
        {
            _dbContext.Cards.Remove(card);
            await _dbContext.SaveChangesAsync();

            return await _dbContext.Cards.ToListAsync();
        }

        public async Task<Card> GetCardByIdAsync(Guid cardId)
        {
            var card = await _dbContext.Cards.FindAsync(cardId);
            if (card == null)
                throw new AdaKanbanException(AdaKanbanExceptionType.NotFound, $"Card not found, cardId: {cardId}");

            return card;
        }

        public async Task<List<Card>> GetAllCardsAsync()
        {
            return await _dbContext.Cards.ToListAsync();
        }
    }
}