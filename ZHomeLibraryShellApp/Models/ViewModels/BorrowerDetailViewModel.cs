using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ZHomeLibraryShellApp.DataAccess.Services;

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

    [ObservableProperty] 
    private string editName;

    [ObservableProperty]
    private string editPhone;

    [ObservableProperty]
    private string editMail;


    public async Task LoadBorrower()
    {
        Borrower = await DbAccess.BorrowerRepo.GetBorrowerById(_borrowerId);

        Books = new ObservableCollection<BookModel>(Borrower.Books);
    }
    
}