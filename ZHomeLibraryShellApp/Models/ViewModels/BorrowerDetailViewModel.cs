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
    private string editName;

    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(UpdateBorrowerInfoCommand))]
    private string editPhone;

    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(UpdateBorrowerInfoCommand))]
    private string editMail;

    [RelayCommand(CanExecute = nameof(UpdateBorrowerInfoCanExecute))]
    private async Task UpdateBorrowerInfo()
    {
        if (!string.IsNullOrEmpty(EditName))
        {
            borrower.Name = EditName;
            EditName = string.Empty;
        }

        if (!string.IsNullOrEmpty(EditPhone))
        {
            borrower.PhoneNo = EditPhone;
            EditPhone = string.Empty;
        }

        if (!string.IsNullOrEmpty(EditMail))
        {
            borrower.Email = EditMail;
            EditMail = string.Empty;
        }

        await DbAccess.BorrowerRepo.UpdateBorrower(borrower);
        await BorrowerManager.OnBookUpdated(borrower);
    }

    private bool UpdateBorrowerInfoCanExecute() //investigate why marked out code freezez application when active
    {
        bool atleastOneFieldWithUpdatedInfo = !string.IsNullOrEmpty(EditName) || !string.IsNullOrEmpty(EditPhone) || !string.IsNullOrEmpty(EditMail);
        //bool nameIsUnique = DbAccess.BorrowerRepo.GetAllBorrowers().Result.All(b => b.Name != EditName);
        //bool nameIsNotEmptyString = string.IsNullOrEmpty(EditName.Trim());

        return atleastOneFieldWithUpdatedInfo; /* && nameIsUnique && nameIsNotEmptyString;*/
    }

    public async Task LoadBorrower()
    {
        Borrower = await DbAccess.BorrowerRepo.GetBorrowerById(_borrowerId);

        Books = new ObservableCollection<BookModel>(Borrower.Books);
    }
    
}