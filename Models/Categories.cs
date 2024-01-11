using System.ComponentModel.DataAnnotations;

namespace SnackboxAPI.Models
{
    public class Categories
    {
        [Key]
        public int category_id { get; set; }

        public string category_name { get; set; }
        public bool available { get; set; }
    }
}