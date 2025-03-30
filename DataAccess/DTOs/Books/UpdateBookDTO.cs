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
    public class UpdateBookDTO
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }

        public string? Description { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string Image { get; set; }

        [Range(1, int.MaxValue)]
        public int Stock { get; set; }

        public Guid CategoryId { get; set; }
    }
}
