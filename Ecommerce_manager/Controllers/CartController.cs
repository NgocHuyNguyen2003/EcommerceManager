using Ecommerce_manager.Data;
using Ecommerce_manager.Helpers;
using Ecommerce_manager.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_manager.Controllers
{
	public class CartController : Controller
	{
		private readonly EcommerceShopContext db;
		public CartController(EcommerceShopContext context) {
			db = context;
		}
		
		public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>> (MySetting.CART_KEY) ?? new List<CartItem>();
		public IActionResult Index()
		{
			return View(Cart);
		}
		public IActionResult AddToCart(int id , int quantity =1)
		{
			var cart = Cart;
			var item = cart.SingleOrDefault(p => p.MaHh == id);
			if(item == null)
			{
				var pro = db.HangHoas.SingleOrDefault(p=>p.MaHh == id);
				if(pro == null)
				{
					TempData["Messgae"] = $"Not found with {id}";
					return Redirect("/404");
				}
				item = new CartItem
				{
					MaHh = pro.MaHh,
					TenHH = pro.TenHh,
					DonGia = pro.DonGia ?? 0,
					Hinh = pro.Hinh ?? string.Empty,
					SoLuong = quantity
				};
				cart.Add(item);
			}
			else
			{
				item.SoLuong += quantity;
			}
			HttpContext.Session.Set(MySetting.CART_KEY, cart);
			return RedirectToAction("index");
			
		}
		public IActionResult RemoveCart(int id)
		{
			var cart = Cart;
            var item = cart.SingleOrDefault(p => p.MaHh == id);
            if(item != null)// if have product
			{
				cart.Remove(item);
                HttpContext.Session.Set(MySetting.CART_KEY, cart);

            }
            return RedirectToAction("index");

        }
    }
}
