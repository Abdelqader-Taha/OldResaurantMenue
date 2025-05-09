using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestMenu.Models
{
    public class MenuItem
    {
        [Key]
        public Guid Id { get; set; }= Guid.NewGuid();

        [ForeignKey(nameof(MenuCatigory))]
        public Guid MenuCategoryID { get; set; }
        public MenuCatigory? MenuCatigory { get; set; }

        [Required]
        public string Name { get; set; }

        public string ImagePath { get; set; }

        public string Description { get; set; }

        public ICollection<Portion> Portions { get; set; } = new List<Portion>();
    }
}
