using ZHomeLibrary.DataAccess.Repositories;
using ZHomeLibrary.Pages;

namespace ZHomeLibrary
{
    public partial class App : Application
    {
        
        public App()
        {
            InitializeComponent();

            var mainPage = new NavigationPage(new MainPage());

            mainPage.BarBackgroundColor = Color.FromArgb("#000000");
            mainPage.BarTextColor = Color.FromArgb("#FFFFFF");

            MainPage = mainPage;

            
        }
    }
}
