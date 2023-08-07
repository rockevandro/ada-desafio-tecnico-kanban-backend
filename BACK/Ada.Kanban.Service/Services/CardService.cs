using Ada.Kanban.Db.Entities;
using Ada.Kanban.Db.Repositories;
using Ada.Kanban.Service.Models;
using AutoMapper;

namespace Ada.Kanban.Service.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IMapper _mapper;

        public CardService(ICardRepository cardRepository, IMapper mapper)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
        }

        public async Task<CardModel> CreateCardAsync(CreateCardModel cardModel)
        {
            var cardEntity = _mapper.Map<Card>(cardModel);
            var createdCardEntity = await _cardRepository.CreateCardAsync(cardEntity);
            var createdCardModel = _mapper.Map<CardModel>(createdCardEntity);

            return createdCardModel;
        }

        public async Task<CardModel> UpdateCardByIdAsync(UpdateCardModel cardModel, Guid cardId)
        {
            var cardEntity = await _cardRepository.GetCardByIdAsync(cardId);
            var cardEntityToUpdate = _mapper.Map(cardModel, cardEntity);
            var updatedCardEntity = await _cardRepository.UpdateCardAsync(cardEntityToUpdate);
            var updatedCardModel = _mapper.Map<CardModel>(updatedCardEntity);

            return updatedCardModel;
        }

        public async Task<List<CardModel>> DeleteCardByIdAsync(Guid cardId)
        {
            var cardEntity = await _cardRepository.GetCardByIdAsync(cardId);
            var cardEntityList = await _cardRepository.DeleteCardAsync(cardEntity);
            var cardModelList = _mapper.Map<List<CardModel>>(cardEntityList);

            return cardModelList;
        }

        public async Task<List<CardModel>> GetAllCardsAsync()
        {
            var cardEntityList = await _cardRepository.GetAllCardsAsync();
            var cardModelList = _mapper.Map<List<CardModel>>(cardEntityList);

            return cardModelList;
        }
    }
}