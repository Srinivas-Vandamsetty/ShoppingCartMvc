﻿using ShoppingCartMvcUI.Models;

namespace ShoppingCartMvcUI.Repositories
{
    // Interface for home repository.
    public interface IHomeRepository
    {
        // Method to fetch books based on search term and genre filter.
        Task<IEnumerable<Book>> GetBooks(string sTerm = "", int genreId = 0);

        // Method to fetch all genres.
        Task<IEnumerable<Genre>> Genres();
    }
}
