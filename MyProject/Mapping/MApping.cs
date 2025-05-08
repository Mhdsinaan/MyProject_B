using AutoMapper;
using MyProject.Models.Product;
using MyProject.Models.User;

namespace MyProject.Mapping
{
    public class MApping:Profile
    {
        public MApping()
        {
            CreateMap<User, LoginDto>().ReverseMap();
            CreateMap<User, RegisterDto>().ReverseMap();
            CreateMap <Product, ProductDto>().ReverseMap();
        }
    }
}
