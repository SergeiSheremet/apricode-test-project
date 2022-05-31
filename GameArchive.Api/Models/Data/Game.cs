namespace GameArchive.Api.Models.Data
{
    public record class Game
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Studio { get; set; }
        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
    }
}
