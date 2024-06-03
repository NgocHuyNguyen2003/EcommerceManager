using AutoMapper;
using Ecommerce_manager.Data;
using Ecommerce_manager.ViewModels;

namespace Ecommerce_manager.Helpers
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<RegisterVM, KhachHang>();
			//.ForMember(kh => kh.HoTen, option => option.MapFrom(RegisterVM => RegisterVM.HoTen))
			//.ReverseMap();//nếu khác tên thì viết rõ
		}
	}
}
