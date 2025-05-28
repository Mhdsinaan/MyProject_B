using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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

        public CartService(MyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CartOUtDto>> GetCartItems(int userId)
        {
            var cartItems = await _context.CartProducts
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .Select(c => new CartOUtDto
                {
                    ProductId=c.ProductId,
                    Name = c.Product.Name,
                    Image = c.Product.Image,
                    Price = (int)c.Product.NewPrice,
                    Quantity = c.Quantity
                })
                .ToListAsync();

            return cartItems;
        }


        public async Task<bool> AddToCart(CartDtos cart, int userId)
        {
            try
            {
                var existingItem = await _context.CartProducts
                    .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == cart.ProductId);

                if (existingItem != null)
                {
                    existingItem.Quantity += cart.Quantity;
                }
                else
                {
                    var cartItem = new CartItems
                    {
                        UserId = userId,
                        ProductId = cart.ProductId,
                        Quantity = cart.Quantity,

                    };

                   
                    await _context.CartProducts.AddAsync(cartItem);
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<CartItems?> RemoveFromCart(int productId, int userId)
        {
            try
            {
                var cartItem = await _context.CartProducts
                    .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

                if (cartItem == null)
                    return null;

                _context.CartProducts.Remove(cartItem);
                await _context.SaveChangesAsync();
                return cartItem;
            }
            catch
            {
                return null;
            }
        }


        public async Task<bool> IncreaseQuantity(int userId, int productId)
        {
            try
            {
                var cartItem = await _context.CartProducts
                    .Include(c => c.Product) // include product to access unit price
                    .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

                if (cartItem == null)
                    return false;

                cartItem.Quantity++;
                cartItem = cartItem.Quantity * cartItem.Product.NewPrice; // update total price

                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }



        public async Task<bool> DecreaseQuantity(int userId, int productId)
        {
            try
            {
                var cartItem = await _context.CartProducts
                    .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

                if (cartItem == null)
                    return false;

                if (cartItem.Quantity <= 1)
                {
                    _context.CartProducts.Remove(cartItem);
                }
                else
                {
                    cartItem.Quantity--;
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
