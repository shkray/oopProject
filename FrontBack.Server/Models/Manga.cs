using System.ComponentModel.DataAnnotations.Schema;

namespace FrontBack.Server.Models
{
    public class Manga
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public float Rating { get; set; }
        public string Url { get; set; }
        public int AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public Author? Author { get; set; }
        public int GenreId { get; set; }
        [ForeignKey(nameof(GenreId))]
        public Genre? Genre { get; set; }

        public void SetRating()
        {

        }
    }

}
