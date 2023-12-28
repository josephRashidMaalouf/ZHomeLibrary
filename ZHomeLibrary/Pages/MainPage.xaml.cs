namespace ZHomeLibrary.Pages;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private void BookBtn_OnClicked(object? sender, EventArgs e)
    {
        if (BookPageView.IsVisible)
        {
            BookPageView.IsVisible = false;
        }
        else
        {
            BookPageView.IsVisible = true;
        }
    }
}