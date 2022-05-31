using Microsoft.EntityFrameworkCore;

namespace GameArchive.Api.Models.Data
{
    [Index(nameof(Name), IsUnique = true)]
    public record class Genre
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
