using System.Collections.ObjectModel;
using ZHomeLibraryShellApp.DataAccess.Services;
using ZHomeLibraryShellApp.Models;

namespace ZHomeLibraryShellApp.Managers;

public class BorrowerManager
{
    public static event Action<BorrowerModel> BorrowerUpdated;
    public static event Action<int> BorrowerDeleted;
    public static event Action<BorrowerModel> BorrowerAdded;

    public static async Task OnBorrowerDeleted(int id)
    {
        BorrowerDeleted?.Invoke(id);
    }

    public static async Task OnBorrowerAdded(BorrowerModel borrower)
    {
        BorrowerAdded?.Invoke(borrower);
    }

    public static async Task OnBorrowerUpdated(BorrowerModel borrower)
    {
        BorrowerUpdated?.Invoke(borrower);
    }

    
}