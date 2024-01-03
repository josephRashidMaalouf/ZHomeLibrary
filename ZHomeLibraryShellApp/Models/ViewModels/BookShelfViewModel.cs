using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZHomeLibraryShellApp.DataAccess.Services;

namespace ZHomeLibraryShellApp.Models.ViewModels;

public partial class BookShelfViewModel: ObservableObject
{
    [ObservableProperty]
    private BookModel book;

    [ObservableProperty]
    private BookModel selectedBook;

    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(AddBookCommand))]
    private string bookTitle;

    [ObservableProperty] private ObservableCollection<BookModel> books = new();

    public BookShelfViewModel()
    {
        LoadBooksAsync();
    }

    [RelayCommand(CanExecute = nameof(AddCommandCanExecute))]
    private async Task AddBook() 
    {
        book.Title = bookTitle;

        var addedBook = await DbAccess.BookRepo.AddNewBook(book.Title, book.AuthorName);

        Books.Add(addedBook);

        bookTitle = string.Empty;
        book = new();
    }


    private bool AddCommandCanExecute()
    {
        //Investigate the canExecute connection to button

        bool titleIsNotEmpty = !string.IsNullOrEmpty(bookTitle);

        //if (Books.Count < 1)
        //    return titleIsNotEmpty;

        bool titleIsUnique = !Books.Any(b => b.Title == bookTitle);

        return titleIsNotEmpty && titleIsUnique;
    }

    private async Task LoadBooksAsync()
    {
        var booksList = await DbAccess.BookRepo.GetAllBooks();
        Books = new ObservableCollection<BookModel>(booksList);
    }
}