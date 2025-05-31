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
        public async Task<IEnumerable<WishlistDto>> AddToWishlist(WishlistDto wishlist, int userId)
        {
            try
            {
                var product = await _context.products.FindAsync(wishlist.ProductId);
                if (product == null) return null; // or throw exception

                bool alreadyExists = await _context.Wishlists
                    .AnyAsync(p => p.ProductId == wishlist.ProductId && p.UserId == userId);

                if (alreadyExists)
                {
                    // Return the current wishlist or throw exception based on your design
                    return await GetWishlistByUserId(userId);
                }

                var newWishlist = new Wishlist
                {
                    UserId = userId,
                    ProductId = wishlist.ProductId
                };

                await _context.Wishlists.AddAsync(newWishlist);
                await _context.SaveChangesAsync();

                // Return updated wishlist
                return await GetWishlistByUserId(userId);
            }
            catch (Exception)
            {
                // Handle error, maybe return empty list or null
                return null;
            }
        }

        private async Task<IEnumerable<WishlistDto>> GetWishlistByUserId(int userId)
        {
            return await _context.Wishlists
                .Where(w => w.UserId == userId)
                .Select(w => new WishlistDto
                {
                    ProductId = w.ProductId,
                    // Map other properties as needed
                }).ToListAsync();
        }




        public async Task<IEnumerable<WishListResDTO>> GetWishlist(int userId)
        {
            try
            {
                var items = await _context.Wishlists
                    .Include(w => w.Product)
                    .Where(w => w.UserId == userId)
                    .ToListAsync();

                if (items == null || !items.Any())
                {
                    return new List<WishListResDTO>(); 
                }

                var wishlistDto = items.Select(w => new WishListResDTO
                {
                    ProductId = w.ProductId,
                    Name = w.Product?.Name,
                    Description = w.Product?.Description,
                    Price = w.Product?.NewPrice ?? 0,
                    Category = w.Product?.Category,
                    Image = w.Product?.Image
                });

                return wishlistDto;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving wishlist: " + ex.Message);
            }
        }


        public async Task<string> RemoveWishlist(int productid, int userId)
        {
            try
            {
                var wishlist = await _context.Wishlists
                    .FirstOrDefaultAsync(p => p.ProductId == productid && p.UserId == userId);
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
