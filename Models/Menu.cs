using System.ComponentModel.DataAnnotations;

namespace SnackboxAPI.Models
{
    public class Menu
    {
        [Key]
        public int id { get; set; }

        public int category_id { get; set; }
        public int item_id { get; set; }
    }

    public class MenuResult
    {
        public string Result { get; set; }
    }
}