using ZHomeLibraryShellApp.Models;

namespace ZHomeLibraryShellApp.Managers;

public class BorrowerManager
{
    public static event Action<BorrowerModel> BorrowerUpdated;

    public static async Task OnBorrowerUpdated(BorrowerModel borrower)
    {
        BorrowerUpdated.Invoke(borrower);
    }
}