using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZHomeLibraryShellApp.DataAccess;
using ZHomeLibraryShellApp.DataAccess.Services;
using ZHomeLibraryShellApp.Managers;
using ZHomeLibraryShellApp.Pages;

namespace ZHomeLibraryShellApp.Models.ViewModels;

public partial class BorrowersViewModel : ObservableObject
{
    [ObservableProperty]
    private BorrowerModel borrower = new();

    [ObservableProperty]
    private BorrowerModel selectedBorrower = new();

    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(AddBorrowerCommand))]
    private string borrowerName;

    [ObservableProperty] private ObservableCollection<BorrowerModel> borrowers = new();

    public BorrowersViewModel()
    {
        LoadBorrowersAsync();

        BorrowerManager.BorrowerUpdated += BorrowerManager_UpdateBorrower;
    }

    private void BorrowerManager_UpdateBorrower(BorrowerModel obj)
    {
        var borrowerToUpdate = Borrowers.FirstOrDefault(b => obj.Id == b.Id);

        if (borrowerToUpdate != null)
        {
            borrowerToUpdate.Name = obj.Name;
            borrowerToUpdate.PhoneNo = obj.PhoneNo;
            borrowerToUpdate.Email = obj.Email;
        }
    }

    [RelayCommand(CanExecute = nameof(AddCommandCanExecute))]
    private async Task AddBorrower()
    {
        borrower.Name = borrowerName;

        var addedBorrower = await DbAccess.BorrowerRepo.AddNewBorrower(borrower.Name, borrower.PhoneNo, borrower.Email);

        Borrowers.Add(addedBorrower);

        borrowerName = string.Empty;
        borrower = new();
    }

    [RelayCommand]
    private void DeleteBorrower(BorrowerModel borrower)
    {
        DbAccess.BorrowerRepo.DeleteBorrower(borrower);
        Borrowers.Remove(borrower);
    }

    [RelayCommand]
    private async Task OpenBorrowerDetailPage()
    {
        await Shell.Current.GoToAsync($"{nameof(BorrowerDetailPage)}?BorrowerId={SelectedBorrower.Id}");
    }

    private bool AddCommandCanExecute()
    {
        bool nameIsNotEmpty = !string.IsNullOrEmpty(borrowerName);
        bool nameIsUnique = !Borrowers.Any(b => b.Name == borrowerName);
        return nameIsNotEmpty && nameIsUnique;
    }

    
    private async Task LoadBorrowersAsync()
    {
        var borrowersList = await DbAccess.BorrowerRepo.GetAllBorrowers();
        Borrowers = new ObservableCollection<BorrowerModel>(borrowersList);
    }

    
}