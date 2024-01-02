using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZHomeLibraryShellApp.DataAccess;
using ZHomeLibraryShellApp.DataAccess.Services;

namespace ZHomeLibraryShellApp.Models.ViewModels;

public partial class BorrowersViewModel : ObservableObject
{
    [ObservableProperty]
    private BorrowerModel borrower = new();

    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(AddBorrowerCommand))]
    private string borrowerName;

    [ObservableProperty] private ObservableCollection<BorrowerModel> borrowers = new();

    public BorrowersViewModel()
    {
        LoadBorrowersAsync();
    }

    [RelayCommand(CanExecute = nameof(AddCommandCanExecute))]
    private async Task AddBorrower()
    {
        borrower.Name = borrowerName;

        await DbAccess.BorrowerRepo.AddNewBorrower(borrower.Name, borrower.PhoneNo, borrower.Email);
        Borrowers.Add(borrower);
        borrower = new();
    }

    private bool AddCommandCanExecute()
    {
        bool nameIsNotEmpty = !string.IsNullOrEmpty(borrowerName);
        bool nameIsUnique = !Borrowers.Any(b => b.Name == borrowerName);
        return nameIsNotEmpty && nameIsUnique;
    }

    [RelayCommand]
    private void DeleteBorrower(BorrowerModel borrower)
    {
        DbAccess.BorrowerRepo.DeleteBorrower(borrower);
        Borrowers.Remove(borrower);
    }

    private async Task LoadBorrowersAsync()
    {
        var borrowersList = await DbAccess.BorrowerRepo.GetAllBorrowers();
        Borrowers = new ObservableCollection<BorrowerModel>(borrowersList);
    }
}