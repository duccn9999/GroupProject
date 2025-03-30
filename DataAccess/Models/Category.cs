using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }

        [Required, StringLength(100)]
        public string CategoryName { get; set; }
        public virtual ICollection<UserCategory> UserCategories { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}