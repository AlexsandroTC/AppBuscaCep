using AppBuscaCEP.Pages;
using AppBuscaCEP.Services.Navigation;
using Xamarin.Forms;

namespace AppBuscaCEP
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new BuscaCepPage();
            //MainPage = new CepsPage();
            //MainPage = new CustomNavigationPage(new CepsPage());
            NavigationService.Current.Initialize();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
