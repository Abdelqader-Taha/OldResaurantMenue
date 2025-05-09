using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestMenu.Models
{
    public class MenuCatigory
    {
        [Key]
        public Guid Id { get; set; }= Guid.NewGuid();

        [Required]
        public string Name { get; set; }

        [ForeignKey(nameof(Menu))]
        public Guid MenuId { get; set; }
        public Menu? Menu { get; set; }

        public string Description { get; set; }

        public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    }
}
