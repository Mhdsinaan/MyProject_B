using AutoMapper;
using MyProject.Models.Cart;
using MyProject.Models.CartModel;
using MyProject.Models.ProductModel;
using MyProject.Models.User;
using MyProject.Models.UserModel;

namespace MyProject.Mapping
{
    public class MApping:Profile
    {
        public MApping()
        {
            CreateMap<Users, LoginDto>().ReverseMap();
            CreateMap<Users, RegisterDto>().ReverseMap();
            CreateMap <Product, ProductDto>().ReverseMap();
            CreateMap<CartItems,CartDtos>().ReverseMap();
        }
    }
}
