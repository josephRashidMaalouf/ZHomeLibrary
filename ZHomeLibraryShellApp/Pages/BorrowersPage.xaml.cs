using ZHomeLibraryShellApp.Models.ViewModels;

namespace ZHomeLibraryShellApp.Pages;

public partial class BorrowersPage : ContentPage
{
	public BorrowersPage()
	{
		InitializeComponent();

        BindingContext = new BorrowersViewModel();
    }
}