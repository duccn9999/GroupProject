using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTOs.Books
{
    public class GetBookDTO
    {
        [Key]
        public Guid BookId { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Stock { get; set; }
        public string CategoryName { get; set; }
    }
}
