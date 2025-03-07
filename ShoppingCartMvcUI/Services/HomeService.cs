using ShoppingCartMvcUI.Models;
using ShoppingCartMvcUI.Repositories;

namespace ShoppingCartMvcUI.Services
{
    // Service class to handle book and genre-related operations
    public class HomeService : IHomeService
    {
        private readonly IHomeRepository _homeRepository;

        // Constructor to inject the home repository
        public HomeService(IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
        }

        // Fetch books based on search term and genre
        public async Task<IEnumerable<Book>> GetBooks(string sTerm = "", int genreId = 0)
        {
            return await _homeRepository.GetBooks(sTerm, genreId);
        }

        // Fetch all genres
        public async Task<IEnumerable<Genre>> GetGenres()
        {
            return await _homeRepository.Genres();
        }
    }
}
