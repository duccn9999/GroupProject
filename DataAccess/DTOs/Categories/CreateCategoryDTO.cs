using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess.DTOs.Categories
{
    public class CreateCategoryDTO
    {
        [JsonIgnore]
        public Guid CategoryId { get; set; }
        [Required, StringLength(100)]
        public string CategoryName { get; set; }
    }
}
