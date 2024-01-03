using ZHomeLibraryShellApp.Models.ViewModels;

namespace ZHomeLibraryShellApp.Pages;

public partial class BookDetailPage : ContentPage
{
	public BookDetailPage()
	{
		InitializeComponent();

        BindingContext = new BookDetailViewModel();
    }
}