using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZHomeLibraryShellApp.DataAccess.Services;
using ZHomeLibraryShellApp.Language;
using ZHomeLibraryShellApp.Managers;
using ZHomeLibraryShellApp.Pages;

namespace ZHomeLibraryShellApp.Models.ViewModels;

[QueryProperty("SelectedBookToDeleteId", "SelectedBookToDeleteId")]
public partial class BookShelfViewModel : ObservableObject
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

    private string _filterPrompt;
    public string FilterPrompt
    {
        get => _filterPrompt;
        set
        {
            if (value == _sortByPrompt) return;
            _sortByPrompt = value;
            var filteredBooks = ListSorting.BookSorter.Filter(value);
            Books = new ObservableCollection<BookModel>(filteredBooks);
            OnPropertyChanged();
        }
    }

    public List<string> FilterPrompts { get; set; }

    private string _sortByPrompt;
    public string SortByPrompt
    {
        get => _sortByPrompt;
        set
        {
            if (value == _sortByPrompt) return;
            _sortByPrompt = value;
            Books = new ObservableCollection<BookModel>(ListSorting.BookSorter.Sort(value, Books.ToList()));
            OnPropertyChanged();
        }
    }
    public List<string> SortByPrompts { get; set; }

    [ObservableProperty]
    private ILanguage language = new English();

    [ObservableProperty]
    private BookModel book = new();

    [ObservableProperty] private BookModel selectedBook = new();

    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(AddBookCommand))]
    private string bookTitle;

    [ObservableProperty] private ObservableCollection<BookModel> books = new();


    public BookShelfViewModel()
    {
        LoadBooksAsync();

        SortByPrompts = new List<string>()
        {
            "Title ascending",
            "Title descending \u2193",
            "Author name ascending \u2191",
            "Author name descending \u2193"
        };

        FilterPrompts = new List<string>()
        {
            "Borrowed",
            "Not borrowed",
            "Show all"
        };
        LanguageManager.LanguageChanged += LanguageManager_LanguageChanged;
        BookManager.BookUpdated += BookManager_UpdateBook;
    }

    private void LanguageManager_LanguageChanged(ILanguage obj)
    {
        Language = obj;
    }

    private void BookManager_UpdateBook(BookModel obj)
    {
        var bookToUpdate = Books.FirstOrDefault(b => obj.Id == b.Id);

        if (bookToUpdate != null)
        {
            bookToUpdate.Title = obj.Title;
            bookToUpdate.AuthorName = obj.AuthorName;
            bookToUpdate.Borrower = obj.Borrower;
        }

    }

    [RelayCommand(CanExecute = nameof(AddCommandCanExecute))]
    private async Task AddBook()
    {
        Book.Title = BookTitle;

        var addedBook = await DbAccess.BookRepo.AddNewBook(Book.Title, Book.AuthorName);

        Books.Add(addedBook);

        BookTitle = string.Empty;
        Book = new();
    }

    private bool AddCommandCanExecute()
    {

        bool titleIsNotEmpty = !string.IsNullOrEmpty(BookTitle);
        bool titleIsUnique = Books.All(b => b.Title != BookTitle);

        return titleIsNotEmpty && titleIsUnique;
    }

    [RelayCommand]
    private async Task OpenBookDetailPage()
    {
        if (SelectedBook == null)
            return;

        await Shell.Current.GoToAsync($"{nameof(BookDetailPage)}?SelectedBookId={SelectedBook.Id}");
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

    public async Task LoadBooksAsync()
    {
        var booksList = await DbAccess.BookRepo.GetAllBooks();
        Books = new ObservableCollection<BookModel>(booksList);
    }
}