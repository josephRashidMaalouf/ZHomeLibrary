using ZHomeLibraryShellApp.Models.ViewModels;

namespace ZHomeLibraryShellApp.Pages;

public partial class LendOutBooksPage : ContentPage
{
	public LendOutBooksPage()
	{
		InitializeComponent();

        BindingContext = new LendOutBooksViewModel();
    }
}