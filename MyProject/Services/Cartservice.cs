using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyProject.Context;
using MyProject.Interfaces;
using MyProject.Models.Cart;
using MyProject.Models.CartModel;

namespace MyProject.Services
{
    public class CartService : ICartproducts
    {
        private readonly MyContext _context;
        private readonly IHttpContextAccessor _IHttpContextAccessor;
        public CartService(MyContext context, IHttpContextAccessor iHttpContextAccessor)
        {
            _context = context;
            _IHttpContextAccessor = iHttpContextAccessor;

        }

        public async Task<string> AddToCart(CartDtos cart)
        {
            try
            {
                var userId = _IHttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId is null)
                {
                    return null;
                }
                int userdata = int.Parse(userId);

                var product = await _context.products.FindAsync(cart.ProductId);
                if (product == null)
                {
                    return null;
                }
                var Existingitem = _context.products.FirstOrDefault(p => p.Id == cart.ProductId && p.Id == cart.ProductId);
                if (Existingitem != null)
                {

                    Existingitem.Quantity += cart.Quantity;
                    _context.products.Update(Existingitem);

                }
                else
                {
                    var cartitem = new CartItems
                    {
                        UserId = userdata,
                        ProductId = cart.ProductId,
                        Quantity = cart.Quantity
                    };
                    _context.CartProducts.Add(cartitem);
                }
                await _context.SaveChangesAsync();
                return "Product added to cart successfully";
            } catch(Exception ex)
            {

                throw new  ("An error occurred while adding the product to the cart", ex);
            }
        }

        public async Task<IEnumerable<CartItems>> GetCartItems(Users userId)
        {
            try
            {
                var cartitem = await _context.CartProducts.Include(p => p.Product).Where(p => p.UserId == userId.Id).ToListAsync();
                if (cartitem == null)
                {
                    return null;
                }
                return cartitem;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the cart items", ex);
            }
        }

        public async Task<CartItems> RemoveFromCart(int id, Users userid)
        {
            try
            {
                var cartitem = _context.CartProducts.FirstOrDefault(p => p.Id == id && p.UserId == userid.Id);
                if (cartitem == null)
                {
                    return null;
                }
                _context.CartProducts.Remove(cartitem);
                await _context.SaveChangesAsync();
                return cartitem;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while removing the product from the cart", ex);
            }
        }

        public async Task<CartItems> UpdateCart(int id, CartDtos cart, Users userId)
        {
            try
            {
                var cartitem = _context.CartProducts.FirstOrDefault(p => p.Id == id && p.UserId == userId.Id);
                if (cartitem == null)
                {
                    return null;
                }
                cartitem.ProductId = cart.ProductId;
                cartitem.Quantity = cart.Quantity;
                await _context.SaveChangesAsync();
                return cartitem;
            }
            catch
            {
                throw new Exception("An error occurred while updating the cart item");
            }
            
        }
    }
}
