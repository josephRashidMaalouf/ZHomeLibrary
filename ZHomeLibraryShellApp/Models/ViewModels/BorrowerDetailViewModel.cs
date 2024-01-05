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

    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(UpdateBorrowerInfoCommand))] 
    private string editName = string.Empty;

    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(UpdateBorrowerInfoCommand))]
    private string editPhone;

    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(UpdateBorrowerInfoCommand))]
    private string editMail;

    [RelayCommand]
    private async Task DeleteBorrower()
    {
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
        var success = await DbAccess.BorrowerRepo.UpdateBorrower(Borrower);

        if (success.success)
        {
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

            await BorrowerManager.OnBorrowerUpdated(Borrower);
        }
        else
        {
            await Shell.Current.DisplayAlert("Could not change name", success.message, "Ok");
        }
    }

    private bool UpdateBorrowerInfoCanExecute() 
    {
        bool atleastOneFieldWithUpdatedInfo = !string.IsNullOrEmpty(EditName) || !string.IsNullOrEmpty(EditPhone) || !string.IsNullOrEmpty(EditMail);
        bool nameIsNotEmptyString = !string.IsNullOrEmpty(EditName.Trim());

        return atleastOneFieldWithUpdatedInfo && nameIsNotEmptyString;
    }

    public async Task LoadBorrower()
    {
        Borrower = await DbAccess.BorrowerRepo.GetBorrowerById(_borrowerId);

        Books = new ObservableCollection<BookModel>(Borrower.Books);
    }
    
}