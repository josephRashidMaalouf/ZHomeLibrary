using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZHomeLibraryShellApp.DataAccess.Services;
using ZHomeLibraryShellApp.Managers;
using ZHomeLibraryShellApp.Pages;

namespace ZHomeLibraryShellApp.Models.ViewModels;

[QueryProperty("SelectedBookToDeleteId", "SelectedBookToDeleteId")]
public partial class BookShelfViewModel: ObservableObject
{

    private int _selectedBookToDeleteId;
    public int SelectedBookToDeleteId
    {
        get => _selectedBookToDeleteId;
        set
        {
            if (Equals(value, _selectedBookToDeleteId)) return;
            _selectedBookToDeleteId = value;

            if (value != 0)
                DeleteSelectedBookAsync();

            OnPropertyChanged();
        }
    }

    [ObservableProperty]
    private BookModel book = new();

    [ObservableProperty] private BookModel selectedBook = new();

    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(AddBookCommand))]
    private string bookTitle;

    [ObservableProperty] private ObservableCollection<BookModel> books = new();
    

    public BookShelfViewModel()
    {
        LoadBooksAsync();

        BookManager.BookUpdated += BookManager_UpdateBook;
    }

    private void BookManager_UpdateBook(BookModel obj)
    {
        var bookToUpdate = Books.FirstOrDefault(b => obj.Id == b.Id);

        if (bookToUpdate != null)
        {
            bookToUpdate.Title = obj.Title;
            bookToUpdate.AuthorName = obj.AuthorName;
        }

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
        if (SelectedBook == null)
            return;

        await Shell.Current.GoToAsync($"{nameof(BookDetailPage)}?SelectedBookId={SelectedBook.Id}");
    }

    private async Task LoadBooksAsync()
    {
        var booksList = await DbAccess.BookRepo.GetAllBooks();
        Books = new ObservableCollection<BookModel>(booksList);
    }

    
    private async Task DeleteSelectedBookAsync()
    {
        await DbAccess.BookRepo.DeleteBook(SelectedBookToDeleteId);

        var bookToDelete = Books.FirstOrDefault(b => b.Id == SelectedBookToDeleteId);
        var bookTitle = string.Empty;
        if (bookToDelete != null)
        {
            bookTitle = bookToDelete.Title;
            Books.Remove(bookToDelete);
        }
            

        await Shell.Current.DisplayAlert("Book deleted", $"{bookTitle} has been successfully deleted from your library", "Ok");
    }
}