using ZHomeLibraryShellApp.Pages;

namespace ZHomeLibraryShellApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(BorrowerDetailPage), typeof(BorrowerDetailPage));
            Routing.RegisterRoute(nameof(BookDetailPage), typeof(BookDetailPage));
        }
    }
}
