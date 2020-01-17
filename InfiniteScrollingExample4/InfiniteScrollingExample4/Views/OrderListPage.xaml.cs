using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfiniteScrollingExample4.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfiniteScrollingExample4.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderListPage : ContentPage
    {
        public OrderListPage()
        {
            InitializeComponent();
            BindingContext = new OrderListViewModel();
        }
    }
}