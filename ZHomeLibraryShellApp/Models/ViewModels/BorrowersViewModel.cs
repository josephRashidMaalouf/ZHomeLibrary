using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZHomeLibraryShellApp.DataAccess;
using ZHomeLibraryShellApp.DataAccess.Services;

namespace ZHomeLibraryShellApp.Models.ViewModels;

public partial class BorrowersViewModel : ObservableObject
{
    private BorrowerRepository BorrowerRepo { get; set; }

    [ObservableProperty]
    private BorrowerModel borrower = new();

    [ObservableProperty] private ObservableCollection<BorrowerModel> borrowers;


    public BorrowersViewModel()
    {
        string dbPath = FileAccessHelper.GetLocalFilePath("borrowers.db");
        BorrowerRepo = new BorrowerRepository(dbPath);
        borrowers = new ObservableCollection<BorrowerModel>(BorrowerRepo.GetAllBorrowers());
    }

    [RelayCommand]
    private void AddBorrower()
    {
        BorrowerRepo.AddNewBorrower(borrower.Name, borrower.PhoneNo, borrower.Email);
        Borrowers.Add(borrower);
        borrower = new();
    }

    [RelayCommand]
    private void DeleteBorrower(BorrowerModel borrower)
    {
        BorrowerRepo.DeleteBorrower(borrower);
        Borrowers.Remove(borrower);
    }
}