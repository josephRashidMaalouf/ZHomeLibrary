using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZHomeLibraryShellApp.DataAccess.Services;
using ZHomeLibraryShellApp.Language;
using ZHomeLibraryShellApp.Managers;
using static System.Reflection.Metadata.BlobBuilder;

namespace ZHomeLibraryShellApp.Models.ViewModels;

[QueryProperty("SelectedBookId", "SelectedBookId")]
public partial class BookDetailViewModel : ObservableObject
{
    private int _selectedBookId;

    [ObservableProperty]
    private ILanguage language;

    [ObservableProperty]
    private BookModel book;

    [ObservableProperty]
    private BorrowerModel borrower = new();

    [ObservableProperty]
    private ObservableCollection<BorrowerModel> borrowers = new();

    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(LendOutBookCommand))]
    private BorrowerModel selectedBorrower = new();

    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(UpdateBookInfoCommand))]
    private string editBookTitle;

    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(UpdateBookInfoCommand))]
    private string editBookAuthor;

    public int SelectedBookId
    {
        get => _selectedBookId;
        set
        {
            if (Equals(value, _selectedBookId)) return;
            _selectedBookId = value;
            LoadBookAsync();
            LoadBorrowersAsync();
            OnPropertyChanged();
        }
    }

    public BookDetailViewModel()
    {
        Language = LanguageManager.CurrentLanguage;
        LanguageManager.LanguageChanged += LanguageManager_LanguageChanged;
    }
    
    private void LanguageManager_LanguageChanged(ILanguage obj)
    {
        Language = obj;
    }
    [RelayCommand]
    private async Task DeleteBook()
    {
        bool bookIsBorrowed = Book.BorrowerId > 0;
        if (bookIsBorrowed)
        {
            var bookBorrowedMsg = Language.GetDeleteBookFailMessage(Book.Borrower.Name);
            await Shell.Current.DisplayAlert(Language.DeleteBook,
                bookBorrowedMsg, Language.Ok);
            return;
        }

        var areYouSureMsg = Language.GetDeleteBookAreYouSureMessage(Book.Title);
        var confirmation = await Shell.Current.DisplayAlert(Language.DeleteBook,
            areYouSureMsg, Language.Yes, Language.No);

        if (confirmation)
            await Shell.Current.GoToAsync($"..?SelectedBookToDeleteId={SelectedBookId}");
        else
            return;
    }

    [RelayCommand(CanExecute = nameof(UpdateBookInfoCanExecute))]
    private async Task UpdateBookInfo()
    {
        var books = await DbAccess.BookRepo.GetAllBooks();

        bool titleOccupied = books.Any(b => b.Title == EditBookTitle);

        if (titleOccupied)
        {
            await Shell.Current.DisplayAlert(Language.CouldNotChangeTitle, Language.YouHaveBookSameTitle, Language.Ok);
            return;
        }

        if (!string.IsNullOrEmpty(EditBookTitle))
        {
            Book.Title = EditBookTitle;
            EditBookTitle = string.Empty;
        }

        if (!string.IsNullOrEmpty(EditBookAuthor))
        {
            Book.AuthorName = EditBookAuthor;
            EditBookAuthor = string.Empty;
        }

        await DbAccess.BookRepo.UpdateBook(Book);
    }

    private bool UpdateBookInfoCanExecute()
    {
        bool entryFieldsNotEmpty = !string.IsNullOrEmpty(EditBookAuthor) || !string.IsNullOrEmpty(EditBookTitle);
        return entryFieldsNotEmpty;
    }

    [RelayCommand(CanExecute = nameof(ReturnBookCanExecute))]
    private async Task ReturnBook()
    {
        await LoanManager.ReturnLoan(Book, Borrower);
        await LoanManager.OnBookReturned(Book);

        var bookReturnedMsg = Language.GetBookReturnedMessage(Book.Title, Borrower.Name);
        await Shell.Current.DisplayAlert(Language.BookReturned, bookReturnedMsg,
            Language.Ok);

        Borrower = new BorrowerModel();
        ReturnBookCommand.NotifyCanExecuteChanged();
        LendOutBookCommand.NotifyCanExecuteChanged();
    }

    private bool ReturnBookCanExecute()
    {
        return Borrower.Id > 0;
    }

    [RelayCommand(CanExecute = nameof(LendOutCanExecute))]
    private async Task LendOutBook()
    {
        await LoanManager.MakeLoan(Book, SelectedBorrower);
        Borrower = SelectedBorrower;

        var loanSuccessMsg = Language.GetLoanSuccessFullMessage(Book.Title, SelectedBorrower.Name);
        await Shell.Current.DisplayAlert(Language.LoanSuccessful, loanSuccessMsg,
            Language.Ok);

        SelectedBorrower = new();

        LendOutBookCommand.NotifyCanExecuteChanged();
        ReturnBookCommand.NotifyCanExecuteChanged();
    }

    private bool LendOutCanExecute()
    {
        bool isNotBorrowed = Borrower.Id == 0;
        bool borrowerSelected = SelectedBorrower is { Id: > 0 };
        return isNotBorrowed && borrowerSelected;
    }

    private async Task LoadBookAsync()
    {
        Book = await DbAccess.BookRepo.GetBookById(_selectedBookId);
        if (Book.BorrowerId > 0)
        {
            Borrower = await DbAccess.BorrowerRepo.GetBorrowerById(Book.BorrowerId);
            ReturnBookCommand.NotifyCanExecuteChanged();
        }
        
    }

    public async Task LoadBorrowersAsync()
    {
        var borrowersList = await DbAccess.BorrowerRepo.GetAllBorrowers();
        Borrowers = new ObservableCollection<BorrowerModel>(borrowersList);
    }
}