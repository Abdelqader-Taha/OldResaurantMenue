using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestMenu.Models
{
    public class Portion
    {
        [Key]
        public Guid Id { get; set; }= Guid.NewGuid();

        [Required]
        public string Size { get; set; }

        public float Price { get; set; }

        [ForeignKey(nameof(MenuItem))]
        public Guid MenuItemId { get; set; }
        public MenuItem? MenuItem { get; set; }

    }
}
