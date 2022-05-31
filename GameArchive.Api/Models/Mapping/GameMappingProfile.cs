using AutoMapper;
using GameArchive.Api.Models.Data;
using GameArchive.Api.Models.Dto;

namespace GameArchive.Api.Models.Mapping
{
    public class GameMappingProfile : Profile
    {
        public GameMappingProfile()
        {
            CreateMap<Game, GameDto>()
                .ForMember(game => game.Genres, opt => opt.MapFrom(game => game.Genres.Select(genre => genre.Name)));
        }
    }
}
