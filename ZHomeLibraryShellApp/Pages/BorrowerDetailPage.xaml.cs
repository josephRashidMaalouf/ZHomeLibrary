using ZHomeLibraryShellApp.Models;
using ZHomeLibraryShellApp.Models.ViewModels;

namespace ZHomeLibraryShellApp.Pages;

public partial class BorrowerDetailPage : ContentPage
{
	public BorrowerDetailPage()
	{
		InitializeComponent();

        BindingContext = new BorrowerDetailViewModel();
    }
}