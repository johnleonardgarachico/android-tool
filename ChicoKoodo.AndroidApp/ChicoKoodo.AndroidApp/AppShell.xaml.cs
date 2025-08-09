using ChicoKoodo.AndroidApp.Pages;

namespace ChicoKoodo.AndroidApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            Routing.RegisterRoute(nameof(NihongoBenkyou), typeof(NihongoBenkyou));
            Routing.RegisterRoute(nameof(NihongoPractice), typeof(NihongoPractice));

            InitializeComponent();
        }
    }
}
