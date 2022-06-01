using GameArchive.Api.Models.Dto;

namespace GameArchive.Api.Services
{
    public interface IGameService
    {
        public Task CreateAsync(GameDto game);
        public Task<GameDto?> ReadByIdAsync(Guid id);
        public Task<IEnumerable<GameDto>> ReadAllAsync();
        public Task<IEnumerable<GameDto>> ReadByGenreAsync(string genre);
        public Task UpdateAsync(GameDto game);
        public Task DeleteAsync(Guid id);
    }
}
