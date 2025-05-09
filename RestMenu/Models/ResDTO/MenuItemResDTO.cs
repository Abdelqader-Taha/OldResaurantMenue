using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestMenu.Models.ResDTO
{
    public class MenuItemResDTO
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

       // [ForeignKey(nameof(MenuCatigory))]
        public Guid MenuCategoryID { get; set; }
        //public MenuCatigory? MenuCatigory { get; set; }

        [Required]
        public string Name { get; set; }

        public string ImagePath { get; set; }

        public string Description { get; set; }

        public ICollection<PortionResDTO> Portions { get; set; } = new List<PortionResDTO>();
    }
}
