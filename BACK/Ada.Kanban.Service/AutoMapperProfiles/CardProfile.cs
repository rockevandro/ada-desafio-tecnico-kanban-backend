using Ada.Kanban.Db.Entities;
using Ada.Kanban.Service.Models;
using AutoMapper;

namespace Ada.Kanban.Service.AutoMapperProfiles
{
    public class CardProfile : Profile
    {
        public CardProfile()
        {
            CreateMap<CardModel, Card>().ReverseMap();
            CreateMap<CreateCardModel, Card>();
            CreateMap<UpdateCardModel, Card>();
        }
    }
}
