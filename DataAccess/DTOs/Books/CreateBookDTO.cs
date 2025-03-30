using System.ComponentModel.DataAnnotations;

namespace DataAccess.DTOs.Books
{
    public class CreateBookDTO
    {

        public string Title { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }

        [Range(1, int.MaxValue)]
        public int Stock { get; set; }

        public Guid CategoryId { get; set; }
    }
}
