using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestMenu.Models
{
    public class Restaurant
    {
        [Key]
        public Guid Id { get; set; }= Guid.NewGuid();

        [Required]
        public string Name { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        public string Url { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User? User { get; set; }

    }
}
