using ZHomeLibrary.Pages;

namespace ZHomeLibrary
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var mainPage = new NavigationPage(new MainPage());

            MainPage = mainPage;
        }
    }
}
