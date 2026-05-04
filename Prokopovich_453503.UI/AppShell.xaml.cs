using Prokopovich_453503.UI.Pages;

namespace Prokopovich_453503.UI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(PirateDetails), typeof(PirateDetails));
        }
    }
}
