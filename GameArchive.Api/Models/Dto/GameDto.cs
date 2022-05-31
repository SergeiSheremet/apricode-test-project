namespace GameArchive.Api.Models.Dto
{
    public record class GameDto
    {
        public Guid? Id { get; init; }
        public string Title { get; init; }
        public string? Studio { get; init; }
        public IEnumerable<string>? Genres { get; init; }
    }
}
