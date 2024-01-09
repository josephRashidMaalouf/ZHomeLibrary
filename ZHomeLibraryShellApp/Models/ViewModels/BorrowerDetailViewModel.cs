using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZHomeLibraryShellApp.DataAccess.Services;
using ZHomeLibraryShellApp.Language;
using ZHomeLibraryShellApp.Managers;

namespace ZHomeLibraryShellApp.Models.ViewModels;

[QueryProperty("BorrowerId", "BorrowerId")]
public partial class BorrowerDetailViewModel : ObservableObject
{
    private int _borrowerId;
    public int BorrowerId
    {
        get => _borrowerId;
        set
        {
            if (value == _borrowerId) return;
            _borrowerId = value;
            LoadBorrower();
            OnPropertyChanged();
        }
    }

    [ObservableProperty] private ILanguage language;

    [ObservableProperty]
    private BorrowerModel borrower;

    [ObservableProperty]
    private ObservableCollection<BookModel> books;

    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(ReturnBooksCommand))]
    private ObservableCollection<Object> selectedBooks = new();

    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(UpdateBorrowerInfoCommand))]
    private string editName = string.Empty;

    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(UpdateBorrowerInfoCommand))]
    private string editPhone;

    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(UpdateBorrowerInfoCommand))]
    private string editMail;

    public BorrowerDetailViewModel()
    {
        Language = LanguageManager.CurrentLanguage;
        LanguageManager.LanguageChanged += LanguageManager_LanguageChanged;
    }
    
    private void LanguageManager_LanguageChanged(ILanguage obj)
    {
        Language = obj;
    }

    [RelayCommand]
    private async Task DeleteBorrower()
    {
        bool borrowerHasActiveLoans = Borrower.Books.Count > 0;
        if (borrowerHasActiveLoans)
        {
            var message = Language.GetDeleteBorrowerFailMessage(Borrower.Books.Count, Borrower.Name);
            await Shell.Current.DisplayAlert(Language.DeleteBorrower,
                message, Language.Ok);
            return;
        }

        var areYouSureMsg = Language.GetDeleteBorrowerAreYouSureMessage(Borrower.Name);
        var confirmation = await Shell.Current.DisplayAlert(Language.DeleteBorrower,
            areYouSureMsg, Language.Yes, Language.No);

        if (confirmation)
            await Shell.Current.GoToAsync($"..?BorrowerToDeleteId={BorrowerId}");
        else
            return;
    }

    [RelayCommand(CanExecute = nameof(UpdateBorrowerInfoCanExecute))]
    private async Task UpdateBorrowerInfo()
    {
        var borrowers = await DbAccess.BorrowerRepo.GetAllBorrowers();

        bool nameOccupied = borrowers.Any(b => b.Name == EditName);

        if (nameOccupied)
        {
            await Shell.Current.DisplayAlert(Language.CouldNotChangeName, Language.ChooseAnotherName, Language.Ok);
            return;
        }

        if (!string.IsNullOrEmpty(EditName))
        {
            Borrower.Name = EditName;
            EditName = string.Empty;
        }

        if (!string.IsNullOrEmpty(EditPhone))
        {
            Borrower.PhoneNo = EditPhone;
            EditPhone = string.Empty;
        }

        if (!string.IsNullOrEmpty(EditMail))
        {
            Borrower.Email = EditMail;
            EditMail = string.Empty;
        }
        await DbAccess.BorrowerRepo.UpdateBorrower(Borrower);
    }

    private bool UpdateBorrowerInfoCanExecute()
    {
        bool atleastOneFieldWithUpdatedInfo = !string.IsNullOrEmpty(EditName) || !string.IsNullOrEmpty(EditPhone) || !string.IsNullOrEmpty(EditMail);
        
        return atleastOneFieldWithUpdatedInfo;
    }

    [RelayCommand(CanExecute = nameof(ReturnBooksCanExecute))]
    private async Task ReturnBooks()
    {
        List<BookModel> books = new();

        foreach (var selectedBook in SelectedBooks)
        {
            if (selectedBook is BookModel book)
            {
                books.Add(book);
            }
        }
        foreach (var book in books)
        {
            Books.Remove(book);
        }

        await LoanManager.ReturnLoan(books.ToArray(), Borrower);

        var returnBookMsg = Language.GetBookReturnedMessage(books.Count, Borrower.Name);
        await Shell.Current.DisplayAlert(Language.BookReturned,returnBookMsg,
            Language.Ok);

        SelectedBooks.Clear();
        ReturnBooksCommand.NotifyCanExecuteChanged();
    }

    private bool ReturnBooksCanExecute()
    {
        return SelectedBooks.Count > 0;
    }

    [RelayCommand]
    private async Task SelectedBooksSelectionChanged()
    {
        ReturnBooksCommand.NotifyCanExecuteChanged();
    }

    public async Task LoadBorrower()
    {
        Borrower = await DbAccess.BorrowerRepo.GetBorrowerById(_borrowerId);

        Books = new ObservableCollection<BookModel>(Borrower.Books);
    }

}