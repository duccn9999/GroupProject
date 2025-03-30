using System.ComponentModel.DataAnnotations;

namespace DataAccess.DTOs.Categories
{
    public class UpdateCategoryDTO
    {
        public Guid CategoryId { get; set; }

        [Required, StringLength(100)]
        public string CategoryName { get; set; }
    }
}
