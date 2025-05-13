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

        public async Task<bool> AddToCart(CartDtos cart,int usreid)
        {
            var userid = await _context.users.FirstOrDefaultAsync(x => x.Id == usreid);
            if (userid == null)
            {
                return false;
            }
            var existingItem = await _context.CartProducts
             .FirstOrDefaultAsync(x => x.UserId==usreid && x.ProductId == cart.ProductId);

            if (existingItem != null)
            {
                return false;
            }
            var newCartitems = new CartItems
            {
                ProductId = cart.ProductId,
                UserId = usreid,
                Quantity = cart.Quantity,


            };
             _context.CartProducts.AddAsync(newCartitems);
            await _context.SaveChangesAsync();
            return true;




        }

        

        public async Task<IEnumerable<CartDtos>> GetCartItems(int userId)
        {
            try
            {
                if (userId == 0)
                {
                    throw new Exception("User not found");
                }
                var cartitems = await _context.CartProducts.Where(p => p.UserId == userId)
                    .Include(p => p.Product)
                    .ToListAsync();
                if (cartitems == null)
                {
                    throw new Exception("Cart items not found");
                }
                var result = _mapper.Map<IEnumerable<CartDtos>>(cartitems);
                return result;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CartItems> incrementCartItems(int id, Product produ)
        {
            var increment= await _context.CartProducts.FirstOrDefaultAsync(p=>p.Id == id && p.ProductId==produ.Id);
            if(increment == null)
            {
                throw new Exception("Cart item not found");
            }   
            increment.Quantity += 1;
            _context.CartProducts.Update(increment);
            await _context.SaveChangesAsync();
            return increment;

        }

        public async Task<CartItems> RemoveFromCart(int id, Users userid)
        {

            var cartitem= await _context.CartProducts.FindAsync(id==userid.Id);
            if (cartitem == null)
            {
                throw new Exception("Cart item not found");
            }
            _context.CartProducts.Remove(cartitem);
            await _context.SaveChangesAsync();
            return cartitem;

        }
    }


}
       