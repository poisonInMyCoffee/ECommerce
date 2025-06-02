using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace shoppingAppRazor_temp.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Category Name")]
        [MaxLength(30)]
        [Required]
        public string Name { get; set; }


        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display Order between 1 to 100")]
        public int DisplayOrder { get; set; }
    }
}
