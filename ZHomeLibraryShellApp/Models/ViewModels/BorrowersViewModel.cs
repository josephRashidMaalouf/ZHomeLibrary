using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZHomeLibraryShellApp.DataAccess;
using ZHomeLibraryShellApp.DataAccess.Services;
using ZHomeLibraryShellApp.Language;
using ZHomeLibraryShellApp.Managers;
using ZHomeLibraryShellApp.Pages;
using static System.Reflection.Metadata.BlobBuilder;

namespace ZHomeLibraryShellApp.Models.ViewModels;

[QueryProperty("BorrowerToDeleteId", "BorrowerToDeleteId")]
public partial class BorrowersViewModel : ObservableObject
{
    [ObservableProperty]
    private ILanguage language;

    [ObservableProperty]
    private BorrowerModel borrower = new();

    [ObservableProperty]
    private BorrowerModel selectedBorrower = new();

    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(AddBorrowerCommand))]
    private string borrowerName;

    [ObservableProperty] private ObservableCollection<BorrowerModel> borrowers = new();
    private int _borrowerToDeleteId;

    public int BorrowerToDeleteId
    {
        get => _borrowerToDeleteId;
        set
        {
            if (value == _borrowerToDeleteId) return;
            _borrowerToDeleteId = value;

            if (value != 0)
                DeleteSelectedBorrowerAsync();

            OnPropertyChanged();
        }
    }

    private int _sortByPrompt;
    public int SortByPrompt
    {
        get => _sortByPrompt;
        set
        {
            if (value == _sortByPrompt) return;
            _sortByPrompt = value;
            Borrowers = new ObservableCollection<BorrowerModel>(ListSorting.BorrowerSorter.Sort(value, Borrowers.ToList()));
            OnPropertyChanged();
        }
    }
    public ObservableCollection<string> SortByPrompts { get; set; }

    public BorrowersViewModel()
    {
        LoadBorrowersAsync();

        Language = LanguageManager.CurrentLanguage;
        LanguageManager.LanguageChanged += LanguageManager_LanguageChanged;

        SortByPrompts = new ObservableCollection<string>()
        {
            Language.NameAsc,
            Language.NameDesc,
            Language.ActiveLoansAsc,
            Language.ActiveLoansDesc
        };
        LanguageManager.LanguageChanged += LanguageManager_LanguageChanged;
        BorrowerManager.BorrowerUpdated += BorrowerManager_UpdateBorrower;
    }

    private void LanguageManager_LanguageChanged(ILanguage obj)
    {
        Language = obj;
        SortByPrompts.Clear();
        SortByPrompts.Add(Language.NameAsc);
        SortByPrompts.Add(Language.NameDesc);
        SortByPrompts.Add(Language.ActiveLoansAsc);
        SortByPrompts.Add(Language.ActiveLoansDesc);
    }
    private void BorrowerManager_UpdateBorrower(BorrowerModel obj)
    {
        var borrowerToUpdate = Borrowers.FirstOrDefault(b => obj.Id == b.Id);

        if (borrowerToUpdate != null)
        {
            borrowerToUpdate.Name = obj.Name;
            borrowerToUpdate.PhoneNo = obj.PhoneNo;
            borrowerToUpdate.Email = obj.Email;
            borrowerToUpdate.Books = obj.Books;
        }
    }

    [RelayCommand(CanExecute = nameof(AddCommandCanExecute))]
    private async Task AddBorrower()
    {
        Borrower.Name = BorrowerName;

        var addedBorrower = await DbAccess.BorrowerRepo.AddNewBorrower(Borrower.Name, Borrower.PhoneNo, Borrower.Email);

        Borrowers.Add(addedBorrower);

        BorrowerName = string.Empty;
        Borrower = new();
    }

    [RelayCommand]
    private void DeleteBorrower(BorrowerModel borrower)
    {
        DbAccess.BorrowerRepo.DeleteBorrower(Borrower.Id);
        Borrowers.Remove(Borrower);
    }

    [RelayCommand]
    private async Task OpenBorrowerDetailPage()
    {
        if (SelectedBorrower == null)
            return;

        await Shell.Current.GoToAsync($"{nameof(BorrowerDetailPage)}?BorrowerId={SelectedBorrower.Id}");
    }

    private bool AddCommandCanExecute()
    {
        bool nameIsNotEmpty = !string.IsNullOrEmpty(BorrowerName);
        bool nameIsUnique = Borrowers.All(b => b.Name != BorrowerName);
        return nameIsNotEmpty && nameIsUnique;
    }


    private async Task DeleteSelectedBorrowerAsync()
    {
        await DbAccess.BorrowerRepo.DeleteBorrower(BorrowerToDeleteId);

        var borrowerToDelete = Borrowers.FirstOrDefault(b => b.Id == BorrowerToDeleteId);
        var borrowerName = string.Empty;
        if (borrowerToDelete != null)
        {
            borrowerName = borrowerToDelete.Name;
            Borrowers.Remove(borrowerToDelete);
        }

        await Shell.Current.DisplayAlert("Borrower deleted", $"{borrowerName} has been successfully deleted from your borrowers list", "Ok");
    }

    public async Task LoadBorrowersAsync()
    {
        var borrowersList = await DbAccess.BorrowerRepo.GetAllBorrowers();
        Borrowers = new ObservableCollection<BorrowerModel>(borrowersList);
    }
}