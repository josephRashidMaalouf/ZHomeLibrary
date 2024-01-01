using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZHomeLibrary.DataAccess.Repositories;

namespace ZHomeLibrary.Models.ViewModels;

public partial class BorrowerViewModel : ObservableObject
{

    private BorrowerRepository BorrowerRepo { get; set; }

    [ObservableProperty] 
    private BorrowerModel borrower = new();

    [ObservableProperty] private ObservableCollection<BorrowerModel> borrowers;


    public BorrowerViewModel()
    {
        string dbPath = FileAccessHelper.GetLocalFilePath("borrowers.db3");
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