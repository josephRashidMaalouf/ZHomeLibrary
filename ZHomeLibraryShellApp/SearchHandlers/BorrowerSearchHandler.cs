using ZHomeLibraryShellApp.DataAccess.Services;
using ZHomeLibraryShellApp.Managers;
using ZHomeLibraryShellApp.Models;
using ZHomeLibraryShellApp.Pages;

namespace ZHomeLibraryShellApp.SearchHandlers;

public class BorrowerSearchHandler : SearchHandler
{
    public List<BorrowerModel> Borrowers { get; set; }

    public BorrowerSearchHandler()
    {
        LoadBorrowersAsync();

        BorrowerManager.BorrowerAdded += BorrowerManager_BorrowerAdded;
        BorrowerManager.BorrowerDeleted += BorrowerManager_BorrowerDeleted;
        BorrowerManager.BorrowerUpdated += BorrowerManager_BorrowerUpdated;
    }

    private void BorrowerManager_BorrowerUpdated(BorrowerModel obj)
    {
        var borrowerToUpdate = Borrowers.FirstOrDefault(b => obj.Id == b.Id);

        if (borrowerToUpdate != null)
        {
            borrowerToUpdate.Name = obj.Name;
        }
    }

    private void BorrowerManager_BorrowerDeleted(int obj)
    {
        var borrowerToDelete = Borrowers.FirstOrDefault(b=> b.Id == obj);
    }

    private void BorrowerManager_BorrowerAdded(BorrowerModel obj)
    {
        Borrowers.Add(obj);
    }

    protected override void OnQueryChanged(string oldValue, string newValue)
    {
        base.OnQueryChanged(oldValue, newValue);

        if (string.IsNullOrWhiteSpace(newValue))
        {
            ItemsSource = null;
        }
        else
        {
            ItemsSource = Borrowers
                .Where(borrower => borrower.Name.ToLower().Contains(newValue.ToLower()))
                .ToList<BorrowerModel>();
        }
    }

    protected override async void OnItemSelected(object item)
    {
        base.OnItemSelected(item);

        // Let the animation complete. 
        await Task.Delay(1000);

        ShellNavigationState state = (App.Current.MainPage as Shell).CurrentState;

        await Shell.Current.GoToAsync($"{nameof(BorrowerDetailPage)}?BorrowerId={((BorrowerModel)item).Id}");
    }

    public async Task LoadBorrowersAsync()
    {
        var borrowersList = await DbAccess.BorrowerRepo.GetAllBorrowers();
        Borrowers = new List<BorrowerModel>(borrowersList);
    }
}