using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MyProject.Context;
using MyProject.Interfaces;
using MyProject.Models.Cart;
using MyProject.Models.CartModel;
using MyProject.Models.ProductModel;

namespace MyProject.Services
{
    public class CartService : ICartproducts
    {
        private readonly MyContext _context;
        private readonly IMapper _mapper;

        public CartService(MyContext context,IMapper mapper)
        {
            _context = context;
           _mapper = mapper;    

        }

        public Task<bool> AddToCart(CartDtos cart,int usreid)
        {
            var userid =_context.users.FirstOrDefault(x => x.Id == usreid);
            if (userid == null)
            {
                return Task.FromResult(false);
            }


        }

        public Task<IEnumerable<CartDtos>> GetCartItems(int userId)
        {
            try
            {
                if (userId == null)
                {
                    throw new Exception("User not found");
                }
                var cartitems=_context.CartProducts.Where(p=>p.UserId == userId)
                    .Include(p => p.Product)    
                    .ToList();
                if (cartitems == null)
                {
                    throw new Exception("Cart items not found");
                }
                return Ok(cartitems);
     
            }
        }

        public Task<CartItems> incrementCartItems(int id, Product produ)
        {
            throw new NotImplementedException();
        }

        public Task<CartItems> RemoveFromCart(int id, Users userid)
        {
            throw new NotImplementedException();
        }
    }


}
       