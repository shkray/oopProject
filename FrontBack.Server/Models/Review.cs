using System.ComponentModel.DataAnnotations.Schema;

namespace FrontBack.Server.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int Grade { get; set; }
        public string Description { get; set; }
        public int MangaId { get; set; }
        [ForeignKey(nameof(MangaId))]
        public Manga? Manga { get; set; }
    }
}
