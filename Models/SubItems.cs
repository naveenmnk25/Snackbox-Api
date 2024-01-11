using System.ComponentModel.DataAnnotations;

namespace SnackboxAPI.Models
{
    public class SubItems
    {
        [Key]
        public int subitem_id { get; set; }

        public string subitem_name { get; set; }

        public int item_id { get; set; }
        public bool available { get; set; }
    }
}