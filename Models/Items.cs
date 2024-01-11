using System.ComponentModel.DataAnnotations;

namespace SnackboxAPI.Models
{
    public class Items
    {
        [Key]
        public int item_id { get; set; }

        public string item_name { get; set; }
        public bool available { get; set; }
    }
}