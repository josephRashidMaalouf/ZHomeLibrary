namespace ZHomeLibrary.Pages;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private void BookBtn_OnClicked(object? sender, EventArgs e)
    {
        BorrowersPageView.IsVisible = false;
        BookPageView.IsVisible = !BookPageView.IsVisible;
    }

    private void BorrowersBtn_OnClicked(object? sender, EventArgs e)
    {
        BookPageView.IsVisible = false;
        BorrowersPageView.IsVisible = !BorrowersPageView.IsVisible;
    }

    private void LendOutBtn_OnClicked(object? sender, EventArgs e)
    {
        Navigation.PushAsync(new BorrowBooksPage());
    }
}