namespace DessertboxAPI.Dto
{
	public class SubitemDto
	{
		public int Subitem_id { get; set; }
		public string Subitem_name { get; set; }
		public bool Available { get; set; }
	}

	public class MenuItemDto
	{
		public int Item_id { get; set; }
		public string ItemType { get; set; }
		public List<SubitemDto> Items { get; set; }
	}

	public class CategoryDto
	{
		public int Category_id { get; set; }
		public string Category_name { get; set; }
		public List<MenuItemDto> MenuItems { get; set; }
	}





}
