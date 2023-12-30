using ZHomeLibrary.DataAccess.Repositories;
using ZHomeLibrary.Pages;

namespace ZHomeLibrary
{
    public partial class App : Application
    {
        public static BorrowerRepository BorrowerRepo { get; private set; }
        public App(BorrowerRepository borrowerRepo)
        {
            InitializeComponent();

            var mainPage = new NavigationPage(new MainPage());

            mainPage.BarBackgroundColor = Color.FromArgb("#000000");
            mainPage.BarTextColor = Color.FromArgb("#FFFFFF");

            MainPage = mainPage;

            BorrowerRepo = borrowerRepo;
        }
    }
}
