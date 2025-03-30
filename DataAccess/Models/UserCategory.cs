using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    [Table("UserCategory")]
    public class UserCategory
    {
        public Guid UserId { get; set; }  // FK to User
        public User User { get; set; }    // Navigation Property

        public Guid CategoryId { get; set; }  // FK to Category
        public Category Category { get; set; }  // Navigation Property
    }
}
