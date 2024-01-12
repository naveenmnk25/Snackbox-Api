using DessertboxAPI.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnackboxAPI.Models;

namespace SnackboxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly ApplicationdbContext _context;

        public MenuController(ApplicationdbContext context)
        {
            _context = context;
        }

        //[HttpGet("Menus")]
        //public async Task<ActionResult<IEnumerable<Menu>>> GetMenus(int category_id)
        //{
        //    var menus = await _context.Menu
        //        .Where(m => m.category_id == category_id)
        //        .Join(_context.Items,
        //            m => m.item_id,
        //            i => i.item_id,
        //            (m, i) => new
        //            {
        //                SelectedCategory = m.category_id,
        //                ItemId = m.item_id,
        //                ItemType = i.item_name
        //            })
        //        .ToListAsync();
        //    if (menus == null || !menus.Any())
        //    {
        //        return NotFound();
        //    }

        //    return Ok(menus);
        //}

        [HttpGet("Categories")]
        public async Task<ActionResult<IEnumerable<Categories>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        [HttpGet("GetMenu")]
        public async Task<string?> GetMenuItems(int categoryId)
        {
            try
            {
                var val = await _context.MenuResults
                    .FromSqlRaw("SP_GetmenuItems {0}", categoryId)
                    .ToListAsync();
                return val[0].Result.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[HttpGet("GetAllCategory")]
		public async Task<ActionResult<List<CategoryDto>>> GetAllCategory()
		{
			var getAdvance = await _context.QueryResult.FromSqlRaw("Execute dbo.SP_GetmenuItems_new").ToListAsync();
			List <CategoryDto> GetAdvance = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CategoryDto>>(getAdvance[0].JsonResult);

			return Ok(GetAdvance);
		}
	}
} 