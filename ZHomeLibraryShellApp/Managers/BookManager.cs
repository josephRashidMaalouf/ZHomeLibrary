using ZHomeLibraryShellApp.Models;

namespace ZHomeLibraryShellApp.Managers;

public static class BookManager
{
    public static event Action<BookModel> BookUpdated;

    public static async Task OnBookUpdated(BookModel book)
    {
        BookUpdated.Invoke(book);
    }
}