using ShoppingCartMvcUI.Models;
using ShoppingCartMvcUI.Repositories;

namespace ShoppingCartMvcUI.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepo;

        public BookService(IBookRepository bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _bookRepo.GetBooks();
        }

        public async Task<Book?> GetBookById(int id)
        {
            return await _bookRepo.GetBookById(id);
        }

        public async Task AddBook(Book book)
        {
            await _bookRepo.AddBook(book);
        }

        public async Task UpdateBook(Book book)
        {
            await _bookRepo.UpdateBook(book);
        }

        public async Task DeleteBook(Book book)
        {
            await _bookRepo.DeleteBook(book);
        }
    }
}
