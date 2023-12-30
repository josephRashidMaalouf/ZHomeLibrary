using ZHomeLibrary.DataAccess.Repositories;
using ZHomeLibrary.Models.ViewModels;

namespace ZHomeLibrary.Pages;

public partial class BorrowersView : ContentView
{
	public BorrowersView()
	{
		InitializeComponent();
        BindingContext = new BorrowerViewModel();
    }
}