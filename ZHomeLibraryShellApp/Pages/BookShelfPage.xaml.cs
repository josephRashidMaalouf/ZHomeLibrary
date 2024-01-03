using ZHomeLibraryShellApp.Models.ViewModels;

namespace ZHomeLibraryShellApp.Pages;

public partial class BookShelfPage : ContentPage
{
	public BookShelfPage()
	{
		InitializeComponent();

        BindingContext = new BookShelfViewModel();
    }
}