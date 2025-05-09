using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MyProject.Context;
using MyProject.Interfaces;
using MyProject.Models.WishlistModel;

namespace MyProject.Services
{
    public class WishlistServices : IWishlistServices
    {
        private readonly MyContext _context;
        public WishlistServices(MyContext context)
        {
            _context = context;
        }
        public async Task<string> AddToWishlist(WishlistDto wishlist, int userId)
        {
            try
            {

                var product = await _context.products.FindAsync(wishlist.ProductId);
                if (product == null) return "Product not found";


                bool alreadyExists = await _context.Wishlists
                    .AnyAsync(p => p.ProductId == wishlist.ProductId && p.UserId == userId);

                if (alreadyExists)
                {
                    return "Product already exists in wishlist";
                }


                var newWishlist = new Wishlist
                {
                    UserId = userId,
                    ProductId = wishlist.ProductId
                };

                await _context.Wishlists.AddAsync(newWishlist);
                await _context.SaveChangesAsync();

                return "Product added to wishlist";
            }
            catch (Exception ex)
            {
                return "An error occurred while adding to wishlist: " + ex.Message;
            }
        }




        public async Task<IEnumerable<Wishlist>> GetWishlist(int userId)
        {
            try
            {
                var items = await _context.Wishlists.Include(p => p.Product)
                    .Where(p => p.UserId == userId)
                    .ToListAsync();
                if (items == null)
                {
                    throw new Exception("No items found in wishlist");
                }
                return items;

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving wishlist: " + ex.Message);
            }


        }

        public async Task<string> RemoveWishlist(int id, int userId)
        {
            try
            {
                var wishlist = await _context.Wishlists
                    .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
                if (wishlist == null) return null;
                _context.Wishlists.Remove(wishlist);
                await _context.SaveChangesAsync();
                return "removed successfully";
            }
            catch (Exception ex)
            {
                return "An error occurred while removing from wishlist: " + ex.Message;
            }
        }
    }
}
