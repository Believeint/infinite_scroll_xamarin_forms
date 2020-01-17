using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InfiniteScrollingExample4.Views;

namespace InfiniteScrollingExample4
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new OrderListPage());
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
