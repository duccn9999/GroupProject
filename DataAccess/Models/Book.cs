using DataAccess.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    [Table("Book")]
    public class Book
    {
        [Key]
        public Guid BookId { get; set; }  // ✅ Primary Key (GUID)

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string Image { get; set; }

        [Range(1, int.MaxValue)]
        public int Stock { get; set; }

        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }

}
