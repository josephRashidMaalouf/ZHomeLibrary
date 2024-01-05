using System.Collections.ObjectModel;
using ZHomeLibraryShellApp.DataAccess.Services;
using ZHomeLibraryShellApp.Models;

namespace ZHomeLibraryShellApp.Managers;

public class BorrowerManager
{
    public static event Action<BorrowerModel> BorrowerUpdated;
    public static event Action<BorrowerModel> BorrowerDeleted;
    public static event Action<BorrowerModel> BorrowerAdded;

    public static async Task OnBorrowerDeleted(BorrowerModel borrower)
    {
        BorrowerDeleted.Invoke(borrower);
    }

    public async Task OnBorrowerAdded(BorrowerModel borrower)
    {
        BorrowerAdded.Invoke(borrower);
    }

    public static async Task OnBorrowerUpdated(BorrowerModel borrower)
    {
        BorrowerUpdated.Invoke(borrower);
    }

    public static async Task LoadBorrowersAsync(ObservableCollection<BorrowerModel> borrowerList)
    {
        var borrowersList = await DbAccess.BorrowerRepo.GetAllBorrowers();
        borrowerList = new ObservableCollection<BorrowerModel>(borrowersList);
    }
}