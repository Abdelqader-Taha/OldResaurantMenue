using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestMenu.Models.ResDTO
{
    public class MenuResDTO
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

      //  [ForeignKey(nameof(Restaurant))]
        public Guid RestaurantId { get; set; }
       // public Restaurant? Restaurant { get; set; }

        public bool IsActive { get; set; }

        public ICollection<MenuCatigoryResDTO> MenuCategories { get; set; } = new List<MenuCatigoryResDTO>();
    }
}
