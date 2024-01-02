using CommunityToolkit.Mvvm.ComponentModel;

namespace ZHomeLibraryShellApp.Models.ViewModels;

[QueryProperty("BorrowerId", "BorrowerId")]
public partial class BorrowerDetailViewModel : ObservableObject
{
    [ObservableProperty]
    private string borrowerId;
}