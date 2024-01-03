using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZHomeLibraryShellApp.DataAccess.Services;
using ZHomeLibraryShellApp.Pages;

namespace ZHomeLibraryShellApp.Models.ViewModels;

public partial class BookShelfViewModel: ObservableObject
{
    [ObservableProperty]
    private BookModel book = new();

    [ObservableProperty] private BookModel selectedBook = new();

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
        
        bool titleIsNotEmpty = !string.IsNullOrEmpty(bookTitle);
        bool titleIsUnique = !Books.Any(b => b.Title == bookTitle);

        return titleIsNotEmpty && titleIsUnique;
    }

    [RelayCommand]
    private async Task OpenBookDetailPage()
    {
        await Shell.Current.GoToAsync($"{nameof(BookDetailPage)}?SelectedBookId={SelectedBook.Id}");
    }

    private async Task LoadBooksAsync()
    {
        var booksList = await DbAccess.BookRepo.GetAllBooks();
        Books = new ObservableCollection<BookModel>(booksList);
    }
}