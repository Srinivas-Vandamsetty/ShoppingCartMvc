using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingCartMvcUI.Models.DTOs;
using ShoppingCartMvcUI.Models;
using ShoppingCartMvcUI.Shared;
using ShoppingCartMvcUI.Services;
using ShoppingCartMvcUI.Repositories;

namespace ShoppingCartMvcUI.Controllers
{
    [RoleAuthorize("Admin")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IGenreRepository _genreRepo;

        public BookController(IBookService bookService, IGenreRepository genreRepo)
        {
            _bookService = bookService;
            _genreRepo = genreRepo;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetBooks();
            return View(books);
        }

        public async Task<IActionResult> AddBook()
        {
            var bookDto = await PrepareBookDTO();
            return View(bookDto);
        }


        [HttpPost]
        public async Task<IActionResult> AddBook(BookDTO bookToAdd)
        {
            if (!ModelState.IsValid) return View(await PrepareBookDTO(bookToAdd));

            try
            {
                await _bookService.AddBook(MapToBook(bookToAdd));
                TempData["successMessage"] = "Book added successfully";
                return RedirectToAction(nameof(AddBook));
            }
            catch
            {
                TempData["errorMessage"] = "An error occurred while adding the book.";
                return View(await PrepareBookDTO(bookToAdd));
            }
        }

        public async Task<IActionResult> UpdateBook(int id)
        {
            var book = await _bookService.GetBookById(id);
            if (book == null) return RedirectToNotFound(id);

            return View(await PrepareBookDTO(MapToDTO(book)));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBook(BookDTO bookToUpdate)
        {
            if (!ModelState.IsValid) return View(await PrepareBookDTO(bookToUpdate));

            try
            {
                await _bookService.UpdateBook(MapToBook(bookToUpdate));
                TempData["successMessage"] = "Book updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["errorMessage"] = "An error occurred while updating the book.";
                return View(await PrepareBookDTO(bookToUpdate));
            }
        }

        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                var book = await _bookService.GetBookById(id);
                if (book == null) return RedirectToNotFound(id);

                await _bookService.DeleteBook(book);
                TempData["successMessage"] = "Book deleted successfully";
            }
            catch
            {
                TempData["errorMessage"] = "An error occurred while deleting the book.";
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<BookDTO> PrepareBookDTO(BookDTO? dto = null)
        {
            dto ??= new BookDTO();
            dto.GenreList = await GetGenreSelectList(dto.GenreId);
            return dto;
        }

        private BookDTO MapToDTO(Book book) => new()
        {
            Id = book.Id,
            BookName = book.BookName,
            AuthorName = book.AuthorName,
            GenreId = book.GenreId,
            Price = book.Price
        };

        private Book MapToBook(BookDTO dto) => new()
        {
            Id = dto.Id,
            BookName = dto.BookName,
            AuthorName = dto.AuthorName,
            GenreId = dto.GenreId,
            Price = dto.Price
        };

        private async Task<IEnumerable<SelectListItem>> GetGenreSelectList(int selectedGenreId = 0) =>
            (await _genreRepo.GetGenres()).Select(g => new SelectListItem
            {
                Text = g.GenreName,
                Value = g.Id.ToString(),
                Selected = g.Id == selectedGenreId
            });

        private IActionResult RedirectToNotFound(int id)
        {
            TempData["errorMessage"] = $"Book with ID {id} not found";
            return RedirectToAction(nameof(Index));
        }
    }
}
