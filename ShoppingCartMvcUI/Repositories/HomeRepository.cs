using Microsoft.EntityFrameworkCore;
using ShoppingCartMvcUI.Data;
using ShoppingCartMvcUI.Models;
using ShoppingCartMvcUI.Repositories;

namespace ShoppingCartMvcUI
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _db;

        // Constructor to inject the database context.
        public HomeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        // Fetch all genres from the database.
        public async Task<IEnumerable<Genre>> Genres()
        {
            return await _db.Genres.ToListAsync();
        }

        // Fetch books based on search term and genre filter.
        public async Task<IEnumerable<Book>> GetBooks(string? sTerm = "", int genreId = 0)
        {
            // Normalize search term: remove extra spaces and convert to lowercase.
            sTerm = sTerm?.Trim().ToLowerInvariant();

            var books = _db.Books
                // Include related Genre details for each book.
                .Include(b => b.Genre)
                .Where(b =>
                    // Apply search filter if sTerm is provided (case-insensitive).
                    (string.IsNullOrEmpty(sTerm) || b.BookName.ToLower().Contains(sTerm)) &&

                    // Apply genre filter if genreId is greater than 0.
                    (genreId == 0 || b.GenreId == genreId))

                // Select only required fields to optimize performance.
                .Select(b => new Book
                {
                    Id = b.Id,
                    Image = b.Image,
                    AuthorName = b.AuthorName, 
                    BookName = b.BookName,
                    GenreId = b.GenreId,    
                    Price = b.Price,           
                    GenreName = b.Genre.GenreName  
                });

            // Execute the query and return the filtered book list.
            return await books.ToListAsync();
        }
    }
}
