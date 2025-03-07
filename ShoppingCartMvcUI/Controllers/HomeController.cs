using Microsoft.AspNetCore.Mvc;
using ShoppingCartMvcUI.Models;
using ShoppingCartMvcUI.Models.DTOs;
using ShoppingCartMvcUI.Services;

namespace ShoppingCartMvcUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        public async Task<IActionResult> Index(string sterm = "", int genreId = 0)
        {
            var books = await _homeService.GetBooks(sterm, genreId);
            var genres = await _homeService.GetGenres();

            var bookModel = new BookDisplayModel
            {
                Books = books,
                Genres = genres,
                STerm = sterm,
                GenreId = genreId
            };

            return View(bookModel);
        }
    }
}
