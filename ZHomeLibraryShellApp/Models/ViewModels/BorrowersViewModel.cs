using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataAccess.Repositories;
using DataAccess.Tables;
using ZHomeLibraryShellApp.DataAccess;

namespace ZHomeLibraryShellApp.Models.ViewModels;

public partial class BorrowersViewModel : ObservableObject
{
    [ObservableProperty]
    private BorrowerTable borrower = new();

    [ObservableProperty] private ObservableCollection<BorrowerTable> borrowers = new(DbAccess.BorrowerRepository.GetAllBorrowers());


    [RelayCommand]
    private void AddBorrower()
    {
        DbAccess.BorrowerRepository.AddNewBorrower(borrower.Name, borrower.PhoneNo, borrower.Email);
        Borrowers.Add(borrower);
        borrower = new();
    }

    [RelayCommand]
    private void DeleteBorrower(BorrowerTable borrower)
    {
        DbAccess.BorrowerRepository.DeleteBorrower(borrower);
        Borrowers.Remove(borrower);
    }
}