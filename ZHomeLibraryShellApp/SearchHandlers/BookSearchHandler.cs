using System.Collections.ObjectModel;
using ZHomeLibraryShellApp.DataAccess.Services;
using ZHomeLibraryShellApp.Managers;
using ZHomeLibraryShellApp.Models;
using ZHomeLibraryShellApp.Pages;

namespace ZHomeLibraryShellApp.SearchHandlers;

public class BookSearchHandler : SearchHandler
{
    public List<BookModel> Books { get; set; }

    public BookSearchHandler()
    {
        LoadBooksAsync();

        BookManager.BookAdded += BookManager_BookAdded;
        BookManager.BookDeleted += BookManager_BookDeleted;
        BookManager.BookUpdated += BookManager_BookUpdated;
    }

    private void BookManager_BookUpdated(BookModel obj)
    {
        var bookToUpdate = Books.FirstOrDefault(b => obj.Id == b.Id);

        if (bookToUpdate != null)
        {
            bookToUpdate.Title = obj.Title;
        }
    }

    private void BookManager_BookDeleted(int obj)
    {
        var book = Books.FirstOrDefault(b => b.Id == obj);

        Books.Remove(book);
    }

    private void BookManager_BookAdded(BookModel obj)
    {
        Books.Add(obj);
    }

    protected override void OnQueryChanged(string oldValue, string newValue)
    {
        base.OnQueryChanged(oldValue, newValue);

        if (string.IsNullOrWhiteSpace(newValue))
        {
            ItemsSource = null;
        }
        else
        {
            ItemsSource = Books
                .Where(book => book.Title.ToLower().Contains(newValue.ToLower()))
                .ToList<BookModel>();
        }
    }

    protected override async void OnItemSelected(object item)
    {
        base.OnItemSelected(item);

        // Let the animation complete.
        await Task.Delay(1000);

        ShellNavigationState state = (App.Current.MainPage as Shell).CurrentState;

        await Shell.Current.GoToAsync($"{nameof(BookDetailPage)}?SelectedBookId={((BookModel)item).Id}");
    }

    public async Task LoadBooksAsync()
    {
        var booksList = await DbAccess.BookRepo.GetAllBooks();
        Books = new List<BookModel>(booksList);
    }

}

