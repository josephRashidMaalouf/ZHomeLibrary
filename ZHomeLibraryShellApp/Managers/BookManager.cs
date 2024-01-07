using System.Collections.ObjectModel;
using ZHomeLibraryShellApp.DataAccess.Services;
using ZHomeLibraryShellApp.Models;

namespace ZHomeLibraryShellApp.Managers;

public static class BookManager
{
    public static event Action<BookModel> BookUpdated;
    public static event Action<int> BookDeleted;
    public static event Action<BookModel> BookAdded;

    public static async Task OnBookUpdated(BookModel book)
    {
        BookUpdated.Invoke(book);
    }

    public static async Task OnBookDeleted(int id)
    {
        BookDeleted.Invoke(id);
    }

    public static async Task OnBookAdded(BookModel book)
    {
        BookAdded.Invoke(book);
    }

    
}