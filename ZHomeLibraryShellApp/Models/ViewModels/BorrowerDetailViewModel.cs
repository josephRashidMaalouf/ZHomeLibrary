using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZHomeLibraryShellApp.DataAccess.Services;
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

    [RelayCommand]
    private async Task DeleteBorrower()
    {
        bool borrowerHasActiveLoans = Borrower.Books.Count > 0;
        if (borrowerHasActiveLoans)
        {
            await Shell.Current.DisplayAlert("Delete borrower",
                $"{Borrower.Name} has {Borrower.Books.Count} active loans. Make sure the books are returned before deleting the borrower", "Ok");
            return;
        }

        var confirmation = await Shell.Current.DisplayAlert("Delete borrower",
            $"Are you sure you want to delete {Borrower.Name} from your borrowers list?", $"Yes, delete {Borrower.Name}", $"No, don't delete {Borrower.Name}");

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
            await Shell.Current.DisplayAlert("Could not change name", "That name is occupied by another borrower. Choose another name.", "Ok");
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
        //await BorrowerManager.OnBorrowerUpdated(Borrower);
    }

    private bool UpdateBorrowerInfoCanExecute()
    {
        bool atleastOneFieldWithUpdatedInfo = !string.IsNullOrEmpty(EditName) || !string.IsNullOrEmpty(EditPhone) || !string.IsNullOrEmpty(EditMail);
        bool nameIsNotEmptyString = !string.IsNullOrEmpty(EditName.Trim());

        return atleastOneFieldWithUpdatedInfo && nameIsNotEmptyString;
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

        Borrower = new();
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