using Ecommerce_manager.Data;
using Ecommerce_manager.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_manager.ViewComponents
{
	public class MenuLoaiViewComponent : ViewComponent
	{
		private readonly EcommerceShopContext db;

		public MenuLoaiViewComponent(EcommerceShopContext context) => db = context;

		public IViewComponentResult Invoke()
		{
			var data = db.Loais.Select(loai => new MenuLoaiVM
			{
				MaLoai = loai.MaLoai,
				TenLoai = loai.TenLoai,
				SoLuong = loai.HangHoas.Count
			}).OrderBy(p => p.TenLoai);

			return View(data); // Default.cshtml
			//return View("Default", data);
		}
	}
}
