using AutoMapper;
using GameArchive.Api.Models.Data;
using GameArchive.Api.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace GameArchive.Api.Services.Implementation
{
    internal class GameService : IGameService
    {
        private readonly ApplicationContext _db;
        private readonly IMapper _mapper;

        public GameService(ApplicationContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }

        public async Task CreateAsync(GameDto game)
        {
            Game insert = new()
            {
                Id = game.Id ?? Guid.NewGuid(),
                Title = game.Title,
                Studio = game.Studio,
                Genres = game.Genres.Where(genre => !_db.Genres.Select(x => x.Name)
                                                               .Contains(genre))
                                    .Select(genre => new Genre { Name = genre })
                                    .Union(_db.Genres.Where(genre => game.Genres.Contains(genre.Name)))
                                    .ToList()
            };

            _db.Games.Add(insert);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<GameDto>> ReadAllAsync()
        {
            return await _db.Games.Select(game => _mapper.Map<GameDto>(game))
                                  .ToListAsync();
        }

        public async Task<GameDto?> ReadByIdAsync(Guid id)
        {
            Game? game = await _db.Games.SingleAsync(g => g.Id == id);
            return _mapper.Map<GameDto>(game);
        }

        public async Task<IEnumerable<GameDto>> ReadByGenreAsync(string genre)
        {
            return await _db.Games.Where(game => game.Genres.Select(g => g.Name)
                                                            .Contains(genre))
                                  .Select(game => _mapper.Map<GameDto>(game))
                                  .ToListAsync();
        }

        public async Task UpdateAsync(GameDto game)
        {
            Game update = await _db.Games.SingleAsync(x => x.Id == game.Id);
            update.Title = game.Title;
            update.Studio = game.Studio;

            update.Genres = game.Genres.Where(genre => !_db.Genres.Select(x => x.Name)
                                                                  .Contains(genre))
                                       .Select(genre => new Genre { Name = genre })
                                       .Union(_db.Genres.Where(genre => game.Genres.Contains(genre.Name)))
                                       .ToList();

            _db.Games.Update(update);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            Game game = new() { Id = id };
            _db.Entry(game).State = EntityState.Deleted;
            await _db.SaveChangesAsync();
        }
    }
}
