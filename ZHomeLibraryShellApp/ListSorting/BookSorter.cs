using System.Collections.ObjectModel;
using ZHomeLibraryShellApp.DataAccess.Services;
using ZHomeLibraryShellApp.Models;

namespace ZHomeLibraryShellApp.ListSorting;

public static class BookSorter
{
    private static List<BookModel> Books { get; set; } = new();


    public static List<BookModel> Sort(string prompt, List<BookModel> books)
    {
        switch (prompt)
        {
            case "Title ascending \u2191":
                return books.OrderBy(b => b.Title).ToList();
            case "Title descending \u2193": 
                return books.OrderByDescending(b => b.Title).ToList();
            case "Author name ascending \u2191":
                return books.OrderBy(b => b.AuthorName).ToList();
            case "Author name descending \u2193":
                return books.OrderByDescending(b => b.AuthorName).ToList();
            default:
                return new List<BookModel>();
        }
    }
    public static List<BookModel> Filter(string prompt)
    {
        LoadBooksAsync();
        switch (prompt)
        {
            case "Borrowed":
                return Books.Where(b => b.BorrowerId > 0).ToList();
            case "Not borrowed":
                return Books.Where(b => b.BorrowerId == 0).ToList();
            case "Show all":
                return Books;
            default:
                return Books;
        }
    }

    private static async Task LoadBooksAsync()
    {
        var booksList = await DbAccess.BookRepo.GetAllBooks();
        Books = new List<BookModel>(booksList);
    }

}




