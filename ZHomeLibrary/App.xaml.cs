using ZHomeLibrary.Pages;

namespace ZHomeLibrary
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }
    }
}
