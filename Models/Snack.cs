using System.ComponentModel.DataAnnotations;

namespace SnackboxAPI.Models
{
    public class Snack
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public bool Available { get; set; }
    }

    public class Dessert
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public bool Available { get; set; }
    }

    public class Addon
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public bool Available { get; set; }
    }

    public class Drink
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public bool Available { get; set; }
    }
}